<template>
	<div>
		<h1-title> {{ name }}</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/administration` }"> Администрирование </router-link></span>
		<span> ➞ </span>
		<span> {{ name }} </span>

		<table-component v-bind:info="info"></table-component>
	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				name: 'Редактирование преподователей',
				info: {
					name: 'Преподаватели',
					titles: [
						{
							name: 'ФИО',
							width: '20%'
						}, {
							name: 'Наименование кафедры',
							width: '28%'
						}, {
							name: 'Наименование должности',
							width: '28%'
						}, {
							name: 'Дата рождения',
							width: '20%'
						}],
					items: []
				}
			}
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
			'table-component': httpVueLoader('/js/components/administration/table-component.vue')
		},
		created: function () {
			axios
				.get(`/api/teachers/GetAdminTeachers`)
				.then(response => {
					this.info.items = response.data;
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
</style>