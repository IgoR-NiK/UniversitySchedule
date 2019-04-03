<template>
	<div>
		<ul>
			<li v-for="item in items">
				<router-link v-bind:to="{ path: `${item.path}` }">{{ item.title }}</router-link>
			</li>
		</ul>
		<div class="welcome" v-if="isAuthenticated">
			Добро пожаловать, {{ secondName }} {{ firstName }}! <button class="logout-button" v-on:click="logoutClick">Выйти</button>
		</div>		
	</div>    
</template>

<script>
	module.exports = {
		data: function () {
			return {
				items: [
					{
						title: 'Главная',
						path: '/index'
					}, {
						title: 'Расписание',
						path: '/faculties'
					}, {
						title: 'Преподаватели',
						path: '/teachers'
					}, {
						title: 'Администрирование',
						path: '/administration'
					}
				]
			}
		},
		computed: {
			firstName: function () {
				return this.$store.state.firstName;
			},
			secondName: function () {
				return this.$store.state.secondName;
			},
			isAuthenticated: function () {
				return this.$store.getters.isAuthenticated;
			}
		},
		methods: {
			logoutClick: function () {
				localStorage.removeItem('user-token');
				localStorage.removeItem('user-firstName');
				localStorage.removeItem('user-secondName');

				this.$store.commit('setToken', '');
				this.$store.commit('setFirstName', '');
				this.$store.commit('setSecondName', '');

				delete axios.defaults.headers.common['Authorization'];

				this.$router.push('/index');
			}
		}
	};
</script>

<style scoped>
	ul {
		margin: 10px 0 0 20px;
		padding: 0;
	}
	
		ul li {
			list-style: none;
			background: transparent;
		}

			ul li a {
				font: normal 22px 'Yanone Kaffeesatz', arial, sans-serif;
				padding: 7px 23px 15px 23px;
				text-decoration: none;
				color: #444;
				display: block;
				float: left;
				height: 20px;
				text-align: center;
				box-sizing: unset;
			}

				ul li a.router-link-active {
					padding: 6px 22px 15px 22px;
					background: #fff;
					border: 1px solid #ddd;
					border-bottom: 0;
					border-radius: 10px 10px 0 0;
					color: #F14E23;
				}

				ul li a:hover {
					color: #F14E23;
				}

	.welcome {
		float: right;
		margin-right: 15px;
		font: normal 16px 'YS Text','Helvetica Neue',Arial,sans-serif;
	}

	.logout-button {
		width: auto;
		cursor: pointer;
		background-color: transparent;
		border: 0;
		padding: 0 20px;
		margin: 5px;
		font: normal 16px 'YS Text','Helvetica Neue',Arial,sans-serif;		
		border-radius: 0;
		outline: none;
	}

		.logout-button:active {
			padding: 0 20px;
			box-shadow: none;
		}

</style>