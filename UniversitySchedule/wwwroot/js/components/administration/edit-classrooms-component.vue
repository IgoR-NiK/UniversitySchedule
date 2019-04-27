<template>
	<div>
		<h1-title>{{ name }}</h1-title>

		<div class="table-box">
			<table class="table" v-bind:width="tableWidth">
				<tr>
					<td colspan="2" rowspan="4"></td>
				</tr>
				<tr>
					<td v-for="week in weeks" v-bind:colspan="weekColSpan(week)">{{ week.name }}</td>
				</tr>
				<tr>
					<template v-for="week in weeks">
						<td v-for="day in week.daysResponse" v-bind:colspan="dayColSpan(day)">{{ day.name }}</td>
					</template>
				</tr>
				<tr>
					<template v-for="week in weeks">
						<template v-for="day in week.daysResponse">
							<td v-for="dayTimeslot in day.dayTimeslotsResponse" v-bind:width="timeslotWidth">{{ dayTimeslot.number }}</td>
						</template>
					</template>
				</tr>

				<template v-for="(building, indexB) in buildings">
					<tr v-for="(room, indexR) in building.classroomsResponse">
						<td v-if="indexR == 0" v-bind:rowspan="buildingRowSpan(building)" v-bind:width="buildingWidth">{{ building.name }}</td>
						<td v-bind:width="classroomWidth">{{ room.name }} ({{ room.capacity }} чел.) {{ room.classroomTypeName }}</td>

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
	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				name: 'Сетка расписания',

				buildingWidth: 30,
				classroomWidth: 125,
				timeslotWidth: 200,

				weeks: [],
				buildings: [],
				cells: []
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
		padding: 3px;
		border: 1px solid #bbb;
	}

	.table td.cellPad {
		padding: 5px;
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
		background-color: #e4fade;
	}

	.practice {
		background-color: #e8f0f7;
	}

	.laboratoryWork {
		background-color: #f7e6fe;
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
</style>