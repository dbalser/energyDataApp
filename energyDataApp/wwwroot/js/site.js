$(document).ready(() => {

	const GetAllData = () => {

		const GetAll = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: '/api/EnergyData'
		}

		$.ajax(GetAll).done((data) => {
			MakeList(data)
		})
  }

	const MakeList = (data, filcol, sortcol) => {

		data.forEach((record) => {

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
									`<p class="MaxPrice">${record.maxPrice ? record.maxPrice : "No data found"}</p>` +

									"<p>Min Price</p>" +
									`<p class="MinPrice">${record.minPrice ? record.minPrice : "No data found"}</p>` +
							"</div>" +

							"<div id='row2'>" +

									"<p>Avg Congestion</p>" +
									`<p class="AvgCongestion">${record.avgCongestion ? record.avgCongestion : "No data found"}</p>` +

									"<p>Max congestion</p>" +
									`<p class="MaxCongestion">${record.maxCongestion ? record.maxCongestion : "No data found"}</p>` +

									"<p>Min Congestion</p>" +
									`<p class="MinCongestion">${record.minCongestion ? record.minCongestion : "No data found"}</p>` +
							"</div>" +
					"</div>" +
			"</li>"
		)

		filcol ? $(`.${filcol}`).css("background", "red") : null
		sortcol ? $(`.${sortcol}`).css("background", "red") : null

		})
	}

	const SortList = (col, format) => {

		const Sort = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${col}/${format}`
		}

		$.ajax(Sort).done((data) => {
			MakeList(data, "", col)
		})
	}

	const FilterList = (col, max, min) => {

		max = max ? max : "0"
		min = min ? min : "0"
		const Filter = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${col}/${max}/${min}`
		}

		$.ajax(Filter).done((data) => {
			MakeList(data, col, "")
		})
	}

	const SortAndFilter = (filcol, max, min, sortcol, format) => {

		max = max ? max : "0"
		min = min ? min : "0"
		const SortFilter = {
			contentType: 'application/json',
			dataType: 'json',
			type: 'GET',
			url: `/api/EnergyData/${filcol}/${max}/${min}/${sortcol}/${format}`
		}

		$.ajax(SortFilter).done((data) => {
			MakeList(data, filcol, sortcol)
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
		$("li").remove()

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
