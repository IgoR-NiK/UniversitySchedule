<template>
	<div>
		<h1-title>Расписание для группы {{ groupName }}</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/faculties` }"> {{ facultyName }} </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses` }"> {{ courseNumber }} курс </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses/${courseNumber}/groups` }"> {{ groupName }} </router-link></span>

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
							<td class="timeslotCol teacherName">{{ dayTimeslot.teacherName }} {{ dayTimeslot.teacherPost }}</td>
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
				facultyId: this.$route.params.facultyId,
				facultyName: '',
				courseNumber: this.$route.params.courseNumber,
				groupId: this.$route.params.groupId,
				groupName: '',
				weeks: []
			}
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
		},
		created: function () {
			axios
				.get(`/api/faculties/${this.facultyId}`)
				.then(response => this.facultyName = response.data.shortName);

			axios
				.get(`/api/groups/${this.groupId}`)
				.then(response => this.groupName = response.data.name);

			axios
				.get(`/api/schedules/GetScheduleForGroup?groupId=${this.groupId}`)
				.then(response => this.weeks = response.data);
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

	.teacherName {
		width: 39%;
	}
</style>