<template>
	<div>
		<h1-title>Выберите группу</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/faculties` }"> {{ facultyName }} </router-link></span>
		<span> ➞ </span>
		<span><router-link v-bind:to="{ path: `/faculties/${facultyId}/courses` }"> {{ courseNumber }} курс </router-link></span>
		<span> ➞ </span>
		<span>Группы</span>

		<div class="list">
			<router-link v-for="group in groups" v-bind:to="{ path: `/faculties/${facultyId}/courses/${courseNumber}/groups/${group.id}/schedule` }" class="list-item">
				<h4> {{ group.name }} </h4>
			</router-link>
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
				groups: []
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
				.get(`/api/groups`, {
					params: {
						facultyId: this.facultyId,
						courseNumber: this.courseNumber
					}
				})
				.then(response => response.data.forEach(x => this.groups.push(x)));
		}
	};
</script>

<style scoped>
	span {
		color: #bbb;
	}

	h4 {
		font-size: 19px;
		font-weight: 400;
		color: #222;
		margin: 5px 0 5px 0;
	}

	a {
		text-decoration: none;
	}

		a.list-item {
			text-decoration: none;
			cursor: pointer;
			display: block;
			padding: 10px 15px;
			margin-bottom: -1px;
			border: 1px solid #dddfe0;
		}

			a.list-item:hover {
				background: linear-gradient(to top, #deedf7, #f2f8fc);
			}

				a.list-item:hover h4 {
					font-size: 21px;
				}

	.list a.list-item:first-child {
		border-radius: 5px 5px 0 0;
		margin-top: 10px;
	}

	.list a.list-item:last-child {
		border-radius: 0 0 5px 5px;
	}
</style>