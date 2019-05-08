<template>
	<div>
		<h1-title>Расписание преподавателей</h1-title>

		<div class="search">
			<label for="search">Введите ФИО преподавателя:</label>
			<input type="text" v-model="search" name="search" />
		</div>

		<div class="teacherTitle">
			Расписание преподавателя <b>{{ teacherName }} {{ teacherPost }}</b>
		</div>		

		<div>
			<div v-for="week in weeks" class="week">
				<p class="weekName">{{ week.name }}</p>
				<div v-for="day in week.daysResponse" class="day">
					<table class="table">
						<tr class="titleDayRow">
							<td colspan="5" class="titleDayCol">
								{{ day.name }}, {{ day.date }}
							</td>
						</tr>
						<tr v-for="dayTimeslot in day.dayTimeslotsResponse" class="timeslotRow">
							<td class="timeslotCol number">{{ dayTimeslot.number }}</td>
							<td class="timeslotCol time">{{ dayTimeslot.startTime }} - {{ dayTimeslot.endTime }}</td>
							<td class="timeslotCol courseName">{{ dayTimeslot.courseName }} {{ dayTimeslot.lessonTypeName }}</td>
							<td class="timeslotCol classroom">{{ dayTimeslot.classroom }}</td>
							<td class="timeslotCol groupName">{{ dayTimeslot.groupName }}</td>
						</tr>
					</table>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				search: '',
				weeks: []
			}
		},
		computed: {
			teacherPost: function () {
				var result = ''

				this.weeks.forEach(x =>
					x.daysResponse.forEach(y =>
						y.dayTimeslotsResponse.forEach(z => {
							if (z.teacherPost)
								result = z.teacherPost;
						})
					)
				);

				return result;
			},
			teacherName: function () {
				var result = ''

				this.weeks.forEach(x =>
					x.daysResponse.forEach(y =>
						y.dayTimeslotsResponse.forEach(z => {
							if (z.teacherName)
								result = z.teacherName;
						})
					)
				);

				return result;
			}
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
		},
		created: function() {
			axios
				.get(`/api/schedules/GetScheduleForTeacher?teacherId=5`)
				.then(response => this.weeks = response.data);
		}
	};
</script>

<style scoped>
	.search label {
		margin-right: 20px;
	}

	.teacherTitle {
		display: table;
		margin: 20px auto;
	}

	.week {
		margin-top: 5px;
	}

	.weekName {
		font-size: 16px;
		font-weight: 600;
		text-align: center;
		margin-top: 0;
	}

	.day {
		margin: 10px 0 30px 0;
	}

	.table {
		width: 100%;
		border-collapse: collapse;
	}

	.titleDayRow {
		font-weight: 600;
		background: linear-gradient(to top, #deedf7, #f2f8fc);
	}

	.titleDayCol {
		padding: 10px 0 10px 15px;
	}

	.timeslotCol {
		text-align: center;
		padding: 10px 0 10px 0;
		border-top: 1px solid #deedf7;
	}

	.number {
		width: 3%;
	}

	.time {
		width: 10%;
	}

	.courseName {
		width: 39%;
	}

	.classroom {
		width: 10%;
	}

	.groupName {
		width: 39%;
	}
</style>