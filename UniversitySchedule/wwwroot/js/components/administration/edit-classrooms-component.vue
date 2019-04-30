<template>
	<div>
		<h1-title>{{ name }}</h1-title>

		<div class="table-box">
			<table class="table" v-bind:width="tableWidth">
				<tr>
					<td colspan="2" rowspan="4"></td>
				</tr>
				<tr>
					<td v-for="week in weeks" v-bind:colspan="weekColSpan(week)"><b>{{ week.name }}</b></td>
				</tr>
				<tr>
					<template v-for="week in weeks">
						<td v-for="day in week.daysResponse" v-bind:colspan="dayColSpan(day)"><b>{{ day.name }}</b></td>
					</template>
				</tr>
				<tr>
					<template v-for="week in weeks">
						<template v-for="day in week.daysResponse">
							<td v-for="dayTimeslot in day.dayTimeslotsResponse" v-bind:width="timeslotWidth"><b>{{ dayTimeslot.number }}</b></td>
						</template>
					</template>
				</tr>

				<template v-for="(building, indexB) in buildings">
					<tr v-for="(room, indexR) in building.classroomsResponse">
						<td v-if="indexR == 0" v-bind:rowspan="buildingRowSpan(building)" v-bind:width="buildingWidth"><b>{{ building.name }}</b></td>
						<td v-bind:width="classroomWidth"><b>{{ room.name }} ({{ room.capacity }} чел.) {{ room.classroomTypeName }}</b></td>

						<td v-for="cell in cells[getIndex(indexB, indexR)]" v-bind:class="[cellColor(cell)]">
							<pre class="preClass">{{ cell.groupName }} 
{{ cell.courseName }}
{{ cell.teacherName }} 
{{ cell.teacherPost }}
</pre>
						</td>

					</tr>
				</template>
			</table>
		</div>

		<table class="legend">
			<tr>
				<td class="box blocked"></td>
				<td class="label"> - Запрет на использование аудитории в таймслоте</td>
				<td class="box lecture"></td>
				<td class="label"> - Лекции</td>
			</tr>
			<tr>
				<td class="box practice"></td>
				<td class="label"> - Практические занятия</td>
				<td class="box laboratoryWork"></td>
				<td class="label"> - Лабораторные работы</td>
			</tr>
		</table>

		<h1-title>Формирование расписания</h1-title>
		<div>
			<button v-on:click="generate" class="green">Сгенерировать</button>
			<button class="red">Стоп</button>
		</div>

		<div id="value-chart"></div>
		<div id="age-chart"></div>

		<h1-title>Информация о расписании</h1-title>
		<div class="evaluation-info">
			<div v-for="evaluation in evaluations"
				 v-bind:class="[{ green : evaluation.currentValue <= evaluation.bestValue }, { red: evaluation.currentValue > evaluation.bestValue }]">
				<i v-if="evaluation.currentValue <= evaluation.bestValue" class="fa fa-check" aria-hidden="true"></i> 
				<i v-else class="fa fa-times fa-lg" aria-hidden="true"></i>
				{{ evaluation.name }} {{ evaluation.currentValue }}
			</div>
		</div>
	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				name: 'Сетка расписания',

				buildingWidth: 30,
				classroomWidth: 150,
				timeslotWidth: 200,

				weeks: [],
				buildings: [],
				cells: [],

				valueChart: {},
				ageChart: {},

				evaluations: [
					{
						name: 'Количество лекций после утренних часов:',
						currentValue: 0,
						bestValue: 0
					}, {
						name: 'Количество избыточных мест в аудиториях:',
						currentValue: 0,
						bestValue: 1000
					}, {
						name: 'Количество превышений дневной нагрузки для всех преподавателей:',
						currentValue: 0,
						bestValue: 0
					}, {
						name: 'Количество "окон" в расписании по всем преподавателям:',
						currentValue: 0,
						bestValue: 0
					}, {
						name: 'Количество превышений дневной нагрузки для всех учебных групп:',
						currentValue: 0,
						bestValue: 0
					}, {
						name: 'Количество "окон" в расписании по всем учебным группам:',
						currentValue: 0,
						bestValue: 0
					}]
			}
		},
		computed: {
			tableWidth: function () {
				var sum = 0;

				for (var i = 0; i < this.weeks.length; i++) {
					sum += this.weekColSpan(this.weeks[i]);
				}

				return sum * this.timeslotWidth + this.buildingWidth + this.classroomWidth;
			}
		},
		methods: {
			generate: function () {
				this.valueChart.series[0].setData([]);
				this.valueChart.series[1].setData([]);
				this.valueChart.xAxis[0].update({
					max: this.valueChart.series[0].data.length + 40
				});

				this.ageChart.series[0].setData([]);
				this.ageChart.xAxis[0].update({
					max: this.valueChart.series[0].data.length + 40
				});

				axios
					.get(`/api/schedules/generateSchedule`);
			},
			dayColSpan: function (day) {
				return day.dayTimeslotsResponse.length;
			},
			weekColSpan: function (week) {
				var sum = 0;

				for (var i = 0; i < week.daysResponse.length; i++) {
					sum += this.dayColSpan(week.daysResponse[i]);
				}

				return sum;
			},
			buildingRowSpan: function (building) {
				return building.classroomsResponse.length;
			},
			cellColor: function (cell) {
				return {
					blocked: cell.cellType == 1,
					lecture: cell.cellType == 2,
					practice: cell.cellType == 3,
					laboratoryWork: cell.cellType == 4,
					cellPad: true
				}
			},
			getIndex: function (indexB, indexR) {
				var result = 0;

				for (var i = 0; i < indexB; i++) {
					result += this.buildings[i].classroomsResponse.length;
				}

				return result + indexR;
			}
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
		},
		created: function () {
			axios
				.get(`/api/schedules/GetScheduleGrid`)
				.then(response => {
					this.weeks = response.data.weeks;
					this.buildings = response.data.buildings;
					this.cells = response.data.cells;
				});

			var hubUrl = 'http://localhost:53941/schedule';
			const hubConnection = new signalR.HubConnectionBuilder()
				.withUrl(hubUrl)
				.configureLogging(signalR.LogLevel.Information)
				.build();

			hubConnection.on("SendScheduleInfo", data => {	
				this.valueChart.series[0].addPoint(data.maxValue);
				this.valueChart.series[1].addPoint(data.averageValue);

				var valuePointCount = this.valueChart.series[0].data.length;
				if (valuePointCount == this.valueChart.xAxis[0].max) {
					this.valueChart.xAxis[0].update({
						max: valuePointCount + 20
					});
				}

				this.ageChart.series[0].addPoint(data.averageAge);
				var agePointCount = this.ageChart.series[0].data.length;
				if (agePointCount == this.ageChart.xAxis[0].max) {
					this.ageChart.xAxis[0].update({
						max: agePointCount + 20
					});
				}

				for (var i = 0; i < data.evaluations.length; i++) {
					this.evaluations[i].currentValue = data.evaluations[i];
				}
			});

			hubConnection.on("GenerateScheduleCompleted", data => {
				this.weeks = data.weeks;
				this.buildings = data.buildings;
				this.cells = data.cells;
			});

			hubConnection.start();
		},
		mounted: function () {
			this.valueChart = Highcharts.chart('value-chart', {
				title: {
					text: 'Максимальная и средняя оценки расписаний'
				},
				yAxis: {
					title: {
						text: 'Оценка расписания'
					}
				},
				legend: {
					layout: 'vertical',
					align: 'right',
					verticalAlign: 'middle'
				},
				xAxis: {
					max: 40
				},
				series: [{
					name: 'Максимальная оценка',
					color: 'green',
					data: [],
					label: {
						enabled: false
					},
					marker: {
						enabled: false
					}
				}, {
					name: 'Средняя оценка',
					color: 'darkorange',
					data: [],
					label: {
						enabled: false
					},
					marker: {
						enabled: false
					}
				}]
			});

			this.ageChart = Highcharts.chart('age-chart', {
				title: {
					text: 'Средний возраст решений в пуле'
				},
				yAxis: {
					title: {
						text: 'Средний возраст'
					}
				},
				legend: {
					layout: 'vertical',
					align: 'right',
					verticalAlign: 'middle'
				},
				xAxis: {
					max: 40
				},
				yAxis: {
					title: {
						text: 'Средний возраст'
					},
					max: 50
				},
				series: [{
					name: 'Средний возраст',
					color: 'blue',
					data: [],
					label: {
						enabled: false
					},
					marker: {
						enabled: false
					}
				}]
			});
		}
	};
</script>

<style scoped>
	span {
		color: #bbb;
	}

	a {
		text-decoration: none;
	}

	.table-box {
		overflow: auto;
		margin: 10px 0;
	}

	.table {
		border-collapse: collapse;
		text-align: center;
		font-size: 14px;
	}

	.table td, tr {
		padding: 7px;
		border: 1px solid #bbb;
	}

	.table td.cellPad {
		padding: 10px;
		line-height: 16px;
	}

		.table td:hover {
			box-shadow: 5px 5px 10px 3px #ddd, -5px -5px 10px 3px #ddd, 5px -5px 10px 3px #ddd, -5px 5px 10px 3px #ddd
		}

	.preClass {
		margin: 0;
		font-family: 'YS Text','Helvetica Neue',Arial,sans-serif;
		font-size: 14px;
	}

	.blocked {
		background-color: #ddd;
	}

	.lecture {
		background-color: #E5FFD5;
	}

	.practice {
		background-color: #D5F6FF;
	}

	.laboratoryWork {
		background-color: #f7e6fe;
	}

	.green {
		color: green;
	}

	.red {
		color: red;
	}

	.box {
		width: 30px;
		height: 30px;
	}

	.label {
		padding-left: 15px;
		width: 400px;
		font-size: 14px;
	}

	.legend {
		margin-bottom: 15px;
	}

	#value-chart {
		max-width: 900px;
		height: 400px;
		margin: 0 auto;
	}

		#value-chart .highcharts-exporting-group, .highcharts-credits {
			display: none;
		}

		#value-chart .highcharts-axis-title {
			color: black;
			font-size: 14px;
		}

	#age-chart {
		max-width: 900px;
		height: 400px;
		margin: 0 auto;
	}

		#age-chart .highcharts-exporting-group, .highcharts-credits {
			display: none;
		}

		#age-chart .highcharts-axis-title {
			color: black;
			font-size: 14px;
		}

	.evaluation-info {
		font-size: 16px;
		font-weight: 600;
		line-height: 28px;
		margin-left: 20px;
	}
</style>