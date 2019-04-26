<template>
	<div>
		<h1-title>Администрирование</h1-title>

		<span>Навигация ::</span>
		<span>Администрирование</span>

		<div class="list">
			<router-link v-for="item in items" v-bind:to="{ path: `${item.path}` }" class="list-item"> 
				<h4> {{ item.name }} </h4>
			</router-link>
		</div>
	</div>	
</template>

<script>
	module.exports = {
		data: function () {
			return {
				items: [
					{
						name: 'Сетка расписания',
						path: '/administration/classrooms'
					},
					{
						name: 'Редактирование факультетов',
						path: '/administration/faculties'
					},
					{
						name: 'Редактирование кафедр',
						path: '/administration/departments'
					}
				]
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