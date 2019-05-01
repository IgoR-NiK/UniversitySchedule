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
				name: 'Редактирование должностей',
				info: {
					name: 'Должности',
					titles: [
						{
							name: 'Наименование',
							width: '53%'
						}, {
							name: 'Описание',
							width: '53%'
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
				.get(`/api/posts/GetAdminPosts`)
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