$(document).ready(() => {

	$(".ErrorMsg").css("display", "none")
	
	// Data from MakeList is stored here to be used again in MakeList
	let CurrentFilCol, CurrentSortCol, CurrentIndex, DataList = []

	const MakeList = (Data, FilCol, SortCol, PrevIndex) => {

		if(Data.length === 0) {
			$("#NoDataError").css("display", "inline-block")
			return
		}

		$(".ErrorMsg").css("display", "none")

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
									`<p class="NodeType">${record.nodeType ? record.nodeType : "No data found"}</p>` +

									"<p>County</p>" +
									`<p class="County">${record.county ? record.county : "No data found"}</p>` +

									"<p>Pricing Type</p>" +
									`<p class="PricingType">${record.pricingType ? record.pricingType : "No data found"}</p>` +
							"</div>" +
					"</div>" +

					"<div id='NumericalInfo'>" +

							"<div id='row1'>" +

									"<p>Avg Price</p>" +
									`<p class="AvgPrice">${record.avgPrice ? record.avgPrice : "No data found"}</p>` +

									"<p>Max Pric</p>" +
									`<p class="MaxPrice">${record.MaxPrice ? record.MaxPrice : "No data found"}</p>` +

									"<p>Min Price</p>" +
									`<p class="MinPrice">${record.MinPrice ? record.MinPrice : "No data found"}</p>` +
							"</div>" +

							"<div id='row2'>" +

									"<p>Avg Congestion</p>" +
									`<p class="AvgCongestion">${record.avgCongestion ? record.avgCongestion : "No data found"}</p>` +

									"<p>Max congestion</p>" +
									`<p class="MaxCongestion">${record.MaxCongestion ? record.MaxCongestion : "No data found"}</p>` +

									"<p>Min Congestion</p>" +
									`<p class="MinCongestion">${record.MinCongestion ? record.MinCongestion : "No data found"}</p>` +
							"</div>" +
					"</div>" +
				"</li>"
			)
		}

		FilCol ? $(`.${FilCol}`).css("background", "red") : null
		SortCol ? $(`.${SortCol}`).css("background", "red") : null

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
		});
	}

	const GetAllData = () => {

		const GetAll = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: '/api/EnergyData'
		}

		$.ajax(GetAll).done((Data) => {
			MakeList(Data, "", "", 0)
		})
  }

	const SortList = (Col, Format) => {

		const Sort = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${Col}/${Format}`
		}

		$.ajax(Sort).done((Data) => {
			$("li").remove()
			MakeList(Data, "", Col, 0)
		})
	}

	const FilterList = (Col, Max, Min) => {

		if(!Max && !Min) {
			$("#FilterError").css("display", "inline-block")
			return
		}
		if(Number(Max) < Number(Min)) {
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

		$.ajax(Filter).done((Data) => {
			$("li").remove()
			MakeList(Data, Col, "", 0)
		})
	}

	const SortAndFilter = (FilCol, Max, Min, SortCol, Format) => {

		if(!Max && !Min) {
			$("#FilterError").css("display", "inline-block")
			return
		}

		Max = Max ? Max : "0"
		Min = Min ? Min : "0"
		const SortFilter = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${FilCol}/${Max}/${Min}/${SortCol}/${Format}`
		}

		$.ajax(SortFilter).done((Data) => {
			$("li").remove()
			MakeList(Data, FilCol, SortCol, 0)
		})
	}


  GetAllData()

	$("#UpdateList").click((e) => {

		const FilterColValue = $('select[name=FilterCol]').val()
		const MaxNumValue = $('input[name=MaxNum]').val()
		const MinNumValue = $('input[name=MinNum]').val()
		const SortColValue = $('select[name=SortCol]').val()
		const SortMethodValue = $('select[name=SortMethod]').val()

		e.preventDefault()

		if(FilterColValue && SortColValue && SortMethodValue) {
			SortAndFilter(FilterColValue, MaxNumValue, MinNumValue, SortColValue, SortMethodValue)
		}

		else if(SortColValue && SortMethodValue) {
			SortList(SortColValue, SortMethodValue)
		}

		else if(FilterColValue) {
			FilterList(FilterColValue, MaxNumValue, MinNumValue)
		}
	})
})
