<template>
	<ul>
		<li v-for="item in items"> {{ item }} </li>		
	</ul>
</template>

<script>
    module.exports = {
		props: ['items']
    };
</script>

<style scoped>
	ul {
		padding: 0;
		list-style: none;
	}

		ul li {
			padding: 8px;
		}

			ul li:before {
				padding-right: 10px;
				font-weight: bold;
				color: #8fdcf5;
				content: "\2981";
				transition-duration: 0.5s;
			}

			ul li:hover:before {
				color: #F14E23;
			}	
</style>