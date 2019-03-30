<template>
	<div>
		<h1-title> {{ name }}</h1-title>

		<span>Навигация ::</span>
		<span><router-link v-bind:to="{ path: `/administration` }"> Администрирование </router-link></span>
		<span> ➞ </span>
		<span> {{ name }} </span>


	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				name: 'Редактирование факультетов'
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