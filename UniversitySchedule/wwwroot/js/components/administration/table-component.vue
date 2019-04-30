<template>
	<div>
		<table class="table title">
			<tr>
				<td class="title-label"> {{ info.name }} </td>
				<td width="8%" class="add-button">
					<table>
						<tr>
							<td>
								<img src="/images/common/add.png" width="20" height="20" />
							</td>
							<td>
								Добавить
							</td>
						</tr>
					</table>
			</tr>
		</table>

		<table class="table main-table">
			<tr class="items-label-row">
				<td v-for="title in info.titles" width="title.width" class="items-label-col"> {{ title.name }} </td>
				<td width="2%"></td>
				<td width="2%"></td>
			</tr>
			<tr v-for="item in info.items" class="item-row">
				<td v-for="value in item" width="title.width" class="item-col"> {{ value }} </td>
				<td class="edit-button">
					<img src="/images/common/edit.png" width="20" height="20" />
				</td>
				<td class="edit-button">
					<img src="/images/common/delete.png" width="20" height="20" />
				</td>
			</tr>
		</table>

	</div>
</template>

<script>
	module.exports = {
		props: ['info']
	};
</script>

<style scoped>
	.table {
		width: 100%;
	}

	.title {
		margin: 20px 0 10px 0;
		border: 1px solid #AED0EA;
		background: linear-gradient(to top, #deedf7, #f2f8fc);
	}

	.title-label {
		padding: 5px;		
		font-weight: 600;
	}

	.add-button {
		border: 1px solid #AED0EA;
		border-radius: 7px;
		color: #2779AA;
		font-size: 12px;
		font-weight: 600;
	}

	.main-table {
		border-collapse: collapse;
	}

	.items-label-row {
		border: 1px solid #AED0EA;
		background: linear-gradient(to top, #deedf7, #f2f8fc);
		color: #2779AA;
		font-weight: 600;
	}

	.items-label-col {
		padding: 7px;
		border: 1px solid #AED0EA;
	}

	.edit-button {
		padding: 3px;
	}

	.item-row {
		border: 1px solid #DDD;
		background-color: #FBFBFB;
	}

	.item-col {
		padding: 5px;
		border: 1px solid #DDD;
	}
</style>