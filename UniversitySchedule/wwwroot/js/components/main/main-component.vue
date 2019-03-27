<template>
    <div id="content">
        <h1>Главная</h1>
        <div style="background: linear-gradient(to right, rgb(255,255,255), rgba(255,255,255,0.35)), url('/images/main/NPI.jpg') no-repeat;background-size: 100%;">
            <div style="float:left;margin:15px">
                <img src="/images/main/emblem.jpg" width="150" height="150" />
            </div>
            <div style="font-size:22px;line-height:150%;font-weight:600;padding:20px 0 20px 30px;width:800px">
                Федеральное государственное бюджетное образовательное учреждение высшего образования <br />
                "Южно-Российский государственный политехнический университет (НПИ) имени М.И. Платова"
            </div>
            <div style="margin:20px 0 0 25px;font-weight:600;font-size:18px;line-height:150%;width:800px">Система разработана для преподавательского состава кафедры "Программное обеспечение вычислительной техники" ЮРГПУ им. М. И. Платова с целью автоматизации делопроизводства и документооборота кафедры</div>
            <div style="margin:20px 0 0 25px;font-weight:600;font-size:18px;line-height:100%;">
                Ключевые функции системы:
                <ul>
                    <li>создание регистрационных карточек документов;</li>
                    <li>поддержка версионности документов;</li>
                    <li>осуществление механизма согласования документов;</li>
                    <li>возможность поиска по актуальным и архивным документам;</li>
                    <li>хранение истории изменений документов;</li>
                    <li>управление правами доступа пользователей к документам и каталогам.</li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>

</script>

<style scoped>
    ul {
        margin: 2px 0 22px 20px;
        padding: 0;
    }

        ul li {
            list-style-type: circle;
            margin: 0 0 6px 0;
            padding: 0 0 4px 25px;
            line-height: 1.5em;
        }    
</style>