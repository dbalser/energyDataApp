$(document).ready(() => {

	const FilterColValue = $('select [name=FilterCol]').val()
	const MaxNumValue = $('select [name=MaxNum]').val()
	const MinNumValue = $('select [name=MinNum]').val()
	const SortColValue = $('select [name=SortCol]').val()
	const SortMethodValue = $('select [name=SortMethod]').val()


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

	const MakeList = (data) => {
        console.log(data)
		data.forEach((record) => {

			$("ul").append(
				"<li>" +

					"<div id='BasicInfo'>" +

							"<div id='row1'>" +
									"<p>Node</p>" +
									`<p>${record.node ? record.node : "No data found"}</p>` +
									"<p>State</p>" +
									`<p>${record.state ? record.state : "No data found"}</p>` +
									"<p>ISO</p>" +
                  `<p>${record.iso ? record.iso : "No data found"}</p>` +
              "</div>" +

							"<div id='row2'>" +
									"<p>Node Type</p>" +
									`<p>${record.nodeType ? record.nodeType : "No data found"}</p>` +
									"<p>County</p>" +
									`<p>${record.county ? record.county : "No data found"}</p>` +
									"<p>Pricing Type</p>" +
									`<p>${record.pricingType ? record.pricingType : "No data found"}</p>` +
							"</div>" +
					"</div>" +

					"<div id='NumericalInfo'>" +

							"<div id='row1'>" +
									"<p>Avg Price</p>" +
									`<p>${record.avgPrice ? record.avgPrice : "No data found"}</p>` +
									"<p>Max Pric</p>" +
									`<p>${record.maxPrice ? record.maxPrice : "No data found"}</p>` +
									"<p>Min Price</p>" +
									`<p>${record.minPrice ? record.minPrice : "No data found"}</p>` +
							"</div>" +

							"<div id='row2'>" +
									"<p>Avg Congestion</p>" +
									`<p>${record.avgCongestion ? record.avgCongestion : "No data found"}</p>` +
									"<p>Max congestion</p>" +
									`<p>${record.maxCongestion ? record.maxCongestion : "No data found"}</p>` +
									"<p>Min Congestion</p>" +
									`<p>${record.minCongestion ? record.minCongestion : "No data found"}</p>` +
							"</div>" +
					"</div>" +
			"</li>"
		)
		})
	}

    if(!FilterColValue, !MaxNumValue, !MinNumValue, !SortColValue, !SortMethodValue) {

        GetAllData()
    }
})
