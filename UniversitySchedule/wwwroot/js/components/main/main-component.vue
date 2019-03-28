<template>
	<div>
		<h1-title>Главная</h1-title>
		<div class="background">
			<div class="emblem">
				<img src="/images/main/emblem.jpg" width="150" height="150" />
			</div>
			<div class="title">
				Федеральное государственное бюджетное образовательное учреждение высшего образования <br />
				"Южно-Российский государственный политехнический университет (НПИ) имени М.И. Платова"
			</div>
			<div class="description">Система разработана для преподавательского состава кафедры "Программное обеспечение вычислительной техники" ЮРГПУ им. М. И. Платова с целью автоматизации делопроизводства и документооборота кафедры</div>
			<div class="list">
				Ключевые функции системы:
				<i-ul v-bind:items="items"></i-ul>
			</div>
		</div>
	</div>
</template>

<script>
	module.exports = {
		data: function () {
			return {
				items: [
					'создание регистрационных карточек документов;',
					'поддержка версионности документов;',
					'осуществление механизма согласования документов;',
					'возможность поиска по актуальным и архивным документам;',
					'хранение истории изменений документов;',
					'управление правами доступа пользователей к документам и каталогам.'
				]
			};
		},
		components: {
			'h1-title': httpVueLoader('/js/components/common/h1-title.vue'),
			'i-ul': httpVueLoader('/js/components/common/i-ul.vue'),
		}
	};
</script>

<style scoped>
	.background {
		background: linear-gradient(to right, rgb(255,255,255), rgba(255,255,255,0.35)), url('/images/main/NPI.jpg') no-repeat;
		background-size: cover, cover;
	}

	.emblem {
		float: left;
		margin: 15px;
	}

	.title {
		font-size: 22px;
		line-height: 150%;
		font-weight: 600;
		padding: 20px 0 20px 30px;
		width: 800px;
	}

	.description {
		margin: 20px 0 0 25px;
		font-weight: 600;
		font-size: 18px;
		line-height: 150%;
		width: 800px;
	}

	.list {
		margin: 20px 0 0 25px;
		font-weight: 600;
		font-size: 18px;
	}
</style>