<template>
	<div>
		<h1-title>Доступ запрещен</h1-title>

		Доступ к данному ресурсу запрещён для вашей роли.
	</div>
</template>

<script>
	module.exports = {		
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
		}
	};
</script>

<style scoped>

</style>