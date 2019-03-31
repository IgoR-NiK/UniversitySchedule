<template>
	<div>
		<h1-title>Выберите курс</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/faculties` }"> {{ facultyName }} </router-link></span>
		<span> ➞ </span>
		<span>Курсы</span>

		<div class="list">
			<router-link v-for="course in courses" v-bind:to="{ path: `/faculties/${facultyId}/courses/${course}/groups` }" class="list-item">
				<h4> {{ course }} курс </h4>
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
				courses: []
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
				.get(`/api/courses?facultyId=${this.facultyId}`)
				.then(response => response.data.forEach(x => this.courses.push(x)));
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
			background-color: #ffffd9;
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