<template>
	<div>
		<h1-title>Выберите факультет</h1-title>

		<span>Навигация ::</span> 
		<span>Факультеты</span>

		<div class="list">
			<router-link v-for="faculty in faculties" v-bind:to="{ path: `/faculties/${faculty.id}/courses` }" class="list-item">
				<h4> {{ faculty.shortName }} </h4>
				<p> {{ faculty.name }}</p>
			</router-link>
		</div>
	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				faculties: []
			}
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
		},
		created: function () {
			axios
				.get('/api/faculties')
				.then(response => response.data.forEach(x => this.faculties.push(x)));
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
		margin: 0 0 10px 0;
	}

	p {
		color: #444;
		margin: 0 0 5px 0;
		font-size: 15px; 
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

			a.list-item:hover p {
				font-size: 16px;
			}

	.list a.list-item:first-child {
		border-radius: 5px 5px 0 0;
		margin-top: 10px;
	}

	.list a.list-item:last-child {
		border-radius: 0 0 5px 5px;
	}
</style>