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
				name: 'Редактирование аудиторий',
				info: {
					name: 'Аудитории',
					titles: [
						{
							name: 'Наименование',
							width: '25%'
						}, {
							name: 'Вместимость',
							width: '25%'
						}, {
							name: 'Тип аудитории',
							width: '30%'
						}, {
							name: 'Наименование корпуса',
							width: '16%'
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
				.get(`/api/classrooms/GetAdminClassrooms`)
				.then(response => {
					response.data.forEach(x => {
						this.info.items.push({
							name: x.name,
							capacity: x.capacity,
							classroomTypeName: x.classroomTypeName,
							buildingName: x.buildingName
						});
					});					
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