<template>
	<div>
		<h1-title>Расписание для группы {{ groupName }}</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/faculties` }"> {{ facultyName }} </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses` }"> {{ courseNumber }} курс </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses/${courseNumber}/groups` }"> {{ groupName }} </router-link></span>


	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				facultyId: this.$route.params.facultyId,
				facultyName: 'ФИТУ',
				courseNumber: this.$route.params.courseNumber,
				groupId: this.$route.params.groupId,
				groupName: 'ФИТУ 3-5Б'				
			}
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
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
</style>