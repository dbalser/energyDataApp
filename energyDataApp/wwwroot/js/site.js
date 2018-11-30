$(document).ready(() => {


	// Data from MakeList is stored here to be used again in MakeList
	let CurrentFilCol, CurrentSortCol, CurrentIndex, DataList = []

	let ResetData = () => {

		CurrentIndex = 0
		CurrentFilCol = ""
		CurrentSortCol = ""
		DataList = []
	}

	const MakeList = (Data, FilCol, SortCol, PrevIndex) => {

		if(Data.length === 0) {
			$("#NoDataError").css("display", "inline-block")
			return
		}

		FilCol ? $(`.${FilCol}`).css("background", "#496E7D") : null
		SortCol ? $(`.${SortCol}`).css("background", "#496E7D") : null

		$(".ErrorMsg").css("display", "none")

		// This data needs stored so when we scroll to the bottom we can send it all recursivly
		CurrentIndex = PrevIndex
		CurrentFilCol = FilCol
		CurrentSortCol = SortCol
		DataList = Data

		// This Checks if we have scrolled near the bottom, if so we spawn the next 10 items in our data
		$(window).scroll(function() {
		   if($(window).scrollTop() + $(window).height() > $(document).height() - 300) {
		       CurrentIndex += 10
					 MakeList(DataList, CurrentFilCol, CurrentSortCol, CurrentIndex)
		   }
		})

		for (var i = PrevIndex; i < 10 + PrevIndex; i++) {

			const record = Data[i]

			if(!record) {
			//This pretects against PrevIndex being too high
				return
			}

			$("ul").append(
				"<li>" +

					"<div id='BasicInfo'>" +

							"<div id='row1'>" +

									"<p>Node</p>" +
									`<p class="Node">${record.node ? record.node : "No data found"}</p>` +

									"<p>State</p>" +
									`<p class="State">${record.state ? record.state : "No data found"}</p>` +

									"<p>ISO</p>" +
                  `<p class="Iso">${record.iso ? record.iso : "No data found"}</p>` +
              "</div>" +

							"<div id='row2'>" +

									"<p>Node Type</p>" +
									`<p class="Nodetype">${record.nodetype ? record.nodetype : "No data found"}</p>` +

									"<p>County</p>" +
									`<p class="County">${record.county ? record.county : "No data found"}</p>` +

									"<p>Pricing Type</p>" +
									`<p class="Pricingtype">${record.pricingtype ? record.pricingtype : "No data found"}</p>` +
							"</div>" +
					"</div>" +

					"<div id='NumericalInfo'>" +

							"<div id='row1'>" +

									"<p>Avg Price</p>" +
									`<p class="Avgprice">${record.avgprice ? record.avgprice : "No data found"}</p>` +

									"<p>Max Pric</p>" +
									`<p class="Maxprice">${record.maxprice ? record.maxprice : "No data found"}</p>` +

									"<p>Min Price</p>" +
									`<p class="Minprice">${record.minprice ? record.minprice : "No data found"}</p>` +
							"</div>" +

							"<div id='row2'>" +

									"<p>Avg Congestion</p>" +
									`<p class="Avgcongestion">${record.avgcongestion ? record.avgcongestion : "No data found"}</p>` +

									"<p>Max congestion</p>" +
									`<p class="Maxcongestion">${record.maxcongestion ? record.maxcongestion : "No data found"}</p>` +

									"<p>Min Congestion</p>" +
									`<p class="Mincongestion">${record.mincongestion ? record.mincongestion : "No data found"}</p>` +
							"</div>" +
					"</div>" +
				"</li>"
			)
		}
	}

	const GetAllData = () => {

		ResetData()

		const GetAll = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: '/api/EnergyData'
		}

		$.ajax(GetAll)
		.done((Data) => {
			$("li").remove()
			MakeList(Data, "", "", 0)
		})
		.fail(() => {
			$("li").remove()
			ResetData()
			MakeList([], "", "", 0)
		})
  }

	const SortList = (Col, Method) => {

		//Error Handling
		if(!Method || !Col) {
			$("#SortError").css("display", "inline-block")
			return
		}

		const Sort = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${Col}/${Method}`
		}

		$.ajax(Sort)
		.done((Data) => {
			$("li").remove()
			MakeList(Data, "", Col, 0)
		})
		.fail(() => {
			$("li").remove()
			ResetData()
			MakeList([], "", "", 0)
		})
	}

	const FilterList = (Col, Max, Min) => {

		//Error Handling
		if(!Col) {
			$("#FilterError").css("display", "inline-block")
			return
		}
		if(!Max && !Min) {
			$("#FilterError").css("display", "inline-block")
			return
		}
		if( Max && Number(Max) < Number(Min)) {
			$("#DifferenceError").css("display", "inline-block")
			return
		}

		Max = Max ? Max : "0"
		Min = Min ? Min : "0"

		const Filter = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${Col}/${Max}/${Min}`
		}

		$.ajax(Filter)
		.done((Data) => {
			$("li").remove()
			MakeList(Data, Col, "", 0)
		})
		.fail(() => {
			$("li").remove()
			ResetData()
			MakeList([], "", "", 0)
		})
	}

	const SortAndFilter = (FilCol, Max, Min, SortCol, Method) => {

		//Error Handling
		let ErrorHit = false
		// If we have a num and no col or just a col and no nums, throw error
		if((!Max && !Min) || (Max && !Min && !FilCol) || (!Max && Min && !FilCol) || !FilCol) {
			$("#FilterError").css("display", "inline-block")
			ErrorHit = true
		}
		// If we have a Max and Max is small the Min, Throw error
		if( Max && Number(Max) < Number(Min)) {
			$("#DifferenceError").css("display", "inline-block")
			ErrorHit = true
		}

		//If were Missing
		if(!Method || !SortCol) {
			$("#SortError").css("display", "inline-block")
			ErrorHit = true
		}

		if(ErrorHit) { return }

		Max = Max ? Max : "0"
		Min = Min ? Min : "0"
		const SortFilter = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${FilCol}/${Max}/${Min}/${SortCol}/${Method}`
		}

		$.ajax(SortFilter)
		.done((Data) => {
			$("li").remove()
			MakeList(Data, FilCol, SortCol, 0)
		})
		.fail(() => {
			$("li").remove()
			ResetData()
			MakeList([], "", "", 0)
		})
	}

	const SubmitBtn_TextHandler = () => {

		$(".Params").change(() => {

			const FilterColValue = $('select[name=FilterCol]').val()
			const MaxNumValue = $('input[name=MaxNum]').val()
			const MinNumValue = $('input[name=MinNum]').val()
			const SortColValue = $('select[name=SortCol]').val()
			const SortMethodValue = $('select[name=SortMethod]').val()

			if(!FilterColValue && !MaxNumValue && !MinNumValue && !SortColValue && !SortMethodValue) {
				$("#UpdateList").val("Get All")
				return
			}

			$("#UpdateList").val("Update list")
		})
	}

	const SubmitBtn_ClickHandler = () => {

		$("#UpdateList").click((e) => {

			$(".ErrorMsg").css("display", "none")

			e.preventDefault()

			const FilterColValue = $('select[name=FilterCol]').val()
			const MaxNumValue = $('input[name=MaxNum]').val()
			const MinNumValue = $('input[name=MinNum]').val()
			const SortColValue = $('select[name=SortCol]').val()
			const SortMethodValue = $('select[name=SortMethod]').val()

			// When no params Get All
			if(!FilterColValue && !MaxNumValue && !MinNumValue && !SortColValue && !SortMethodValue) {
				GetAllData()
				return
			}
			// If a param from both sides gets filled out but not all
			if((FilterColValue || MaxNumValue || MinNumValue) && (SortColValue || SortMethodValue)) {
				SortAndFilter(FilterColValue, MaxNumValue, MinNumValue, SortColValue, SortMethodValue)
			}
			// If one or more sort params is filled
			else if(SortColValue || SortMethodValue) {
				SortList(SortColValue, SortMethodValue)
			}
			// If one or more sort params is filled
			else if(FilterColValue || MaxNumValue || MinNumValue) {
				FilterList(FilterColValue, MaxNumValue, MinNumValue)
			}
		})
	}

	// On load Get All
  GetAllData()

	SubmitBtn_TextHandler()

	SubmitBtn_ClickHandler()

})
