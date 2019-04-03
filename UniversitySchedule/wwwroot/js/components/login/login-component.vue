<template>
	<div>
		<h1-title>Выполнить вход</h1-title>
		<div class="login-container">
			<div>Для входа в раздел "Администрирование" введите логин и пароль</div>
			<div class="area-inputs">
				<div class="login-row">
					<label for="login">Логин:</label>
					<input type="text" v-model="login" name="login" placeholder="Логин" />
				</div>
				<div class="login-row">
					<label for="password">Пароль:</label>
					<input type="password" v-model="password" name="password" placeholder="Пароль" />
				</div>
				<div class="login-row">
					<button v-on:click="loginClick">Войти</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	module.exports = {
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue')			
		},
		data: function () {
			return {
				login: '',
				password: ''
			}
		},
		methods: {
			loginClick: function () {
				axios
					.post('/api/login', {
						login: this.login,
						password: this.password
					})
					.then(response => {	
						localStorage.setItem('user-token', response.data.token);
						localStorage.setItem('user-firstName', response.data.firstName);
						localStorage.setItem('user-secondName', response.data.secondName);

						this.$store.commit('setToken', response.data.token);
						this.$store.commit('setFirstName', response.data.firstName);
						this.$store.commit('setSecondName', response.data.secondName);	

						axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;

						this.$router.push('/administration');
					})
					.catch(error => {
						localStorage.removeItem('user-token');
						localStorage.removeItem('user-firstName');
						localStorage.removeItem('user-secondName');

						this.$store.commit('setToken', '');
						this.$store.commit('setFirstName', '');
						this.$store.commit('setSecondName', '');	

						delete axios.defaults.headers.common['Authorization'];
					})
			}
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