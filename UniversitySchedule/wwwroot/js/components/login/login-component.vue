<template>
	<div>
		<h1-title>Выполнить вход</h1-title>
		<div class="login-container">
			<div>Для входа в раздел "Администрирование" введите логин и пароль"</div>
			<div class="area-inputs">
				<div class="login-row">
					<label for="login">Логин:</label>
					<input type="text" name="login" placeholder="Логин" />
				</div>
				<div class="login-row">
					<label for="password">Пароль:</label>
					<input type="text" name="password" placeholder="Пароль" />
				</div>
				<div class="login-row">
					<button>Войти</button>
				</div>
			</div>	
		</div>
	</div>
</template>

<script>
	module.exports = {
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue')			
		}
	};
</script>

<style scoped>	
	.login-container {
		width: 510px;
		margin: 0 auto;		
		margin-top: 5px;
	}

		.login-container label {
			width: 120px;
			display: inline-block;
			margin-top: 5px;
			float: left;
			padding: 3px;
		}		

		.login-container button {
			margin-left: 330px;
		}	

	.area-inputs {
		margin-top: 15px;
		width: 85%; 
		margin: 15px auto 15px;
	}

	.login-row {
		padding: 10px 0;
	}
</style>