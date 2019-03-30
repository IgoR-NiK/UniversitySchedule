<template>
	<div>
		<h1-title>Расписание для группы {{ groupName }}</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/faculties` }"> {{ facultyName }} </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses` }"> {{ courseId }} курс </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses/${courseId}/groups` }"> {{ groupName }} </router-link></span>


	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				facultyId: this.$route.params.facultyId,
				facultyName: 'ФИТУ',
				courseId: this.$route.params.courseId,
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