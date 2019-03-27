<template>
    <ul>
        <li v-for="item in items"><router-link v-bind:to="{ path: `${item.path}` }">{{ item.title }}</router-link></li>
        <li><a href="/home/administration">Администрирование</a></li>
    </ul>
</template>

<script>
    module.exports = {
        data: function () {
            return {
                items: [{
                    title: 'Главная',
                    path: '/index'
                }, {
                    title: 'Cтуденты',
                    path: '/studentShedule'
                }, {
                    title: 'Преподаватели',
                    path: '/teacherShedule'
                }]
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
            font: normal 135% 'Yanone Kaffeesatz', arial, sans-serif;
            padding: 7px 23px 15px 23px;
            text-decoration: none;
            color: #444;
            display: block;
            float: left;
            height: 20px;
            text-align: center;
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
</style>