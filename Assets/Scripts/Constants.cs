﻿using System;

public static class Constants {

    // DB-Related Constants
    public const string DBUsersPath = "users";
    public const string DBTasksPath = "tasks";
    public const string DBAnswersPath = "answers";
    public const string DBSolvedTasksPath = "solved";
    public const string DBTaskAnswersField = "Answers";
    public const string DBTaskCorrectAnswersField = "CorrectAnswers";

    // Time Constants
    public static readonly DateTime BeginingOfTime = new DateTime(2020, 1, 1, 0, 0, 0);
    public static readonly int[] TimerIntervalSeconds = {60, 20, 60};
    
    // Skill names
    public static readonly string[] SkillsNames = {
        /* 0 */ "Работа с людьми",
        /* 1 */ "Межотраслевая коммуникация",
        /* 2 */ "Навыки художественного творчества",
        /* 3 */ "Работа в условиях неопределенности",
        /* 4 */ "Управление проектами",
        /* 5 */ "Экологическое мышление"
    };

    // Scene Names
    public const string SnAuth = "Auth";
    public const string SnIntro = "Intro";
    public const string SnMain = "Main";
    public const string SnFinal = "Final";
    public const string SnTutorial = "Tutorial";

    // Characters Name
    public static readonly string[] CharactersNames = {
        "Жизненная цель - природа",
        "Жизненная цель - искусство",
        "Жизненная цель - общение",
        "Жизненная цель - технический специалист",
        "Жизненная цель - упорядоченность системы"
    };
    
    public const string AuthUserNotFound = "Неверный логин или пароль!";
    public const string AuthPasswordsDontMatch = "Пароли не совпадают!";
    public const string AuthRegistrationSuccess = "Успешная регистрация!";
    public const string AuthRegistrationFail = "Некорректные логин или пароль!";
    public const string AuthRestoreSuccess = "Письмо со сбросом пароля отправлено!";
    public const string AuthRestoreFail = "Такого пользоватаеля нет в системе!";

    public const string IntroFinalStringNotReady = "Неплохой результат! Однако, тебе ещё есть над чем поработать, " +
                                                       "обрати внимание на свои слабые стороны и постарайся улучшить эти " +
                                                       " дисциплины для получения паспорта!";
    
    public const string IntroFinalStringReady = "Ого! Похоже ты и так уже готов получить свой паспорт! Что ж, " +
                                                   "давай проведём финальную проверку, и узнаем, действительно ли ты " +
                                                   "так силен в этих знаниях!";

    public static readonly string[] UserTypesNames = {
        "Ученик",
        "Учитель",
        "Админ"
    };
    
    public static readonly string[] TaskTypesNames = {
        "Теория",
        "Практика",
        "Кейс-Задание"
    };

    public static readonly string[] UserTypeToSceneName = {
        SnMain,
        "Teacher",
        "Admin"
    };

    public static readonly string[] CharactersImageLink = {
        "Characters/EcoFemale",
        "Characters/ArtFemale",
        "Characters/SocialFemale",
        "Characters/TechFemale",
        "Characters/SystemsFemale",
        "Characters/EcoMale",
        "Characters/ArtMale",
        "Characters/SocialMale",
        "Characters/TechMale",
        "Characters/SystemsMale"
    };
    
    public static readonly string[] CharactersShortImageLink = {
        "Characters/EcoFemaleShort",
        "Characters/ArtFemaleShort",
        "Characters/SocialFemaleShort",
        "Characters/TechFemaleShort",
        "Characters/SystemsFemaleShort",
        "Characters/EcoMaleShort",
        "Characters/ArtMaleShort",
        "Characters/SocialMaleShort",
        "Characters/TechMaleShort",
        "Characters/SystemsMaleShort"
    };

    public static readonly string[] UserTypeToButtonText = {
        "Сделать учителем",
        "Сделать учеником"
    };

    public static readonly string[] SkillsImageLink = {
        /* 0 */ "SkillsIcons/PeopleWork",
        /* 1 */ "SkillsIcons/InterCommunication",
        /* 2 */ "SkillsIcons/ArtSkills",
        /* 3 */ "SkillsIcons/StressWork",
        /* 4 */ "SkillsIcons/ProjectsManagement",
        /* 5 */ "SkillsIcons/EcoThinking"
    };

    public static readonly float[][] RequiredSkillsToPass = {
        // Ряд - компетенция, столбец навык.
        // Навыки в том же порядке как в PP_SKILLS_NAMES

        // Жизненная цель - природа
        new[] { 80f, 80f, 40f, 60f, 100f, 100f },

        // Жизненная цель - искусство
        new[] { 80f, 60f, 100f, 60f, 100f, 40f },

        // Жизненная цель - общение
        new[] { 100f, 80f, 60f, 100f, 100f, 60f },

        // Жизненная цель - технический
        new[] { 60f, 100f, 80f, 100f, 100f, 60f },

        // Жизненная цель - упорядоченность системы
        new[] { 60f, 40f, 40f, 80f, 100f, 40f }
    };

////// CHARACTERS DESCRIPTION //////
    
    public static readonly string[] CharactersDesc = {
        // Жизненная цель - природа
        "Интересы: Любит собак, кошек или хомячков больше, чем людей. Главный предмет внимания – растения, животные и их среда обитания.\n\n" +
        "Предметы: биология и/или география\n\n" +
        "Характер: инициативный, самостоятельный, заботливый и дальновидный.\n\n" +
        "Поведение: любит рассказывать и слушать истории о животных или природе. Ухаживает за домашними или растениями.\n\n" +
        "Стремится: к гармонии с окружающей средой.\n\n" +
        "Его ждет успех в таких профессиях как: микробиолог, архитектор живых систем," +
        "урбанист-эколог, сити-фермер, ГМО-агроном, космобиолог.\n\n" +
        "Мне приносит удовольствие общение с питомцами и уход за ними, свободное время при любой погоде я предпочитаю проводить на природе. " + 
        "У меня есть, как минимум, одно растение, которым я горжусь! Мне говорят, что я наблюдателен и терпелив.",

        // Жизненная цель - искусство
        "Интересы: искусство в разных проявлениях (арт, театр, музыка и т.д.).\n\n" +
        "Предметы: ИЗО, музыка, технология.\n\n" +
        "Характер: креативность (оригинальный, нестандартный ум), эстетические чувства, любовь к красоте, богатое воображение.\n\n" +
        "Поведение: может сделать или сказать что-нибудь необычное.\n\n" +
        "Мне говорят, что я творческий на всю голову. Я делаю – значит я самовыражаюсь. Могу найти долю креатива даже в " +
        "работе по шаблону. Придумаю всё, что угодно, а потом попробую это воплотить в жизнь. Должно быть красиво. " +
        "Мыслю образами, наблюдаю. Мои творения могут поменять твое настроение.",

        // Жизненная цель - общение
        "Интересы: обучение, воспитание, обслуживание, руководство.\n\n" +
        "Предметы: история, литература, обществознание.\n\n" +
        "Характер: общительность, эрудированность, обаятельность.\n\n" +
        "Поведение: Легко общается с новыми людьми. Хороший слушатель. Обычно улыбается, когда входит в помещение, где есть люди.\n\n" +
        "Выслушаю, вникну в ситуацию, войду в положение, помогу. Не тянет убивать после общения с большим количеством людей. Или " +
        "тянет, но умею сдерживаться и расслабляться. С легкостью убеждаю и веду за собой. Новые люди – как глоток свежего воздуха. Люблю " +
        "быть полезным. Оратор от бога, выдержка и самообладание королевские. Компромисс – моё второе я.",

        // Жизненная цель - технический
        "Интересы: Интересуют технические объекты: машины, механизмы, материалы, виды энергии. Легко справляется с техническими новшествами.\n\n" +
        "Предметы: физика, химия, математика и/или черчение.\n\n" +
        "Характер: точность, дисциплинированность, творческий подход, техническая фантазия.\n\n" +
        "Поведение: быстро разбирается с работой новых технических устройств. " +
        "В курсе открытий в сфере робототехники, машиностроения или освоения космоса.\n\n" +
        "Его ждет успех в таких профессиях как: проектировщик «умной среды», проектировщик космических сооружений, проектировщик " +
        "роботов, конструктор новых материалов, проектировщик беспилотной авиации.\n\n" +
        "Я обожаю всё ремонтировать, даже если не совсем представляю, как оно устроено! В свободное время я буду " +
        "конструировать что-то, что облегчит жизнь членам моей семьи. Я точно знаю, как сделать лучше, быстрее, " +
        "качественнее. Я умею концентрироваться, воспринимаю информацию четко и точно.",

        // Жизненная цель - упорядоченность системы
        "Интересы: книги, новости в соц.сетях, программирование.\n\n" +
        "Предметы: математика, информатика, иностранные языки.\n\n" +
        "Характер: внимательный, анализирующий, скурпулезный.\n\n" +
        "Поведение: достаточно легко могу удерживать концентрацию на важном предмете, что бы ни происходило вокруг. " +
        "У меня порядок: рубашки в шкафу висят по цветам, папки на компьютере структурированы. Могу быстро перерабатывать " +
        "огромные потоки информации и не бешусь, если она однообразна. Я вижу красоту мира в последовательностях и " +
        "логике. Усидчив, терпелив, сконцентрирован на задаче. Память – мое второе я."
    };
    
////// INTRO //////    

    // Intro Quiz Questions
    public static readonly string[][] IntroQuizQuestions = {
        // Работа с людьми
        new [] {
            "Легко знакомлюсь с новыми людьми.",
            "У меня много друзей и приятелей.",
            "Предпочитаю фильмы и книги, где есть личные истории.",
            "Принимаю участие в общественных работах в школе.",
            "ВКонтакте у меня сотни людей.",
            "Ко мне часто обращаются прохожие на улице, и я охотно помогаю им найти дорогу.",
            "Ко мне часто обращаются с просьбами, чтобы я что-то объяснил, обращаются за советом, как разрешить конфликты между людьми и т.п."
        },
        
        // Межотраслевая коммуникация
        new [] {
            "Люблю общаться с новыми людьми.",
            "Я достаточно хорошо общаюсь как со сверстниками, так и с теми, кто старше или младше меня.",
            "Мне интеерсно узнавать о новых темах, предметах, людях.",
            "Смогу найти общую тему для разговора практически с любым человеком – как с физиком, так и с филологом.",
            "Меня зовут на помощь, чтобы разрешить конфликт или непонимание между разными людьми.",
            "Мне нравятся совершенно противоположные предметы, например, математика и литература, химия и история.",
            "Я слежу за новыми изобретениями из разных областей: IT и медицина, строительство и нанотехнологии, наука и искусство и т.п."
        },
        
        // Навыки художественного творчества
        new[] {
            "Люблю все красивое.",
            "Я часто что-то рисую или мастерю красивые вещи.",
            "Я интересуюсь модой.",
            "С удовольствием занималаюсь в художественной школе и на уроках ИЗО.",
            "Часто, чтобы что-то запомнить, я делаю схемы или зарисовки материала.",
            "При просмотре фильма, я часто представляю в мыслях, как бы мог снять сам.",
            "Ко мне обращаются за советами по выбору гардероба или просят что-то нарисовать."
        },

        // Работа в условиях неопределенности
        new [] {
            "Меня сложно вывести из равновесия.",
            "В критической ситуации (экзамен, задача, которую нужно сделать быстро и пр.) я действую даже эффективнее.",
            "Мне интересно искать новые подходы решения задач, договариваться с кем-то, придумывать и делать что-то новое.",
            "Я быстро принимаю решение при недостатке времени и информации.",
            "Я быстро переключаюсь с выполнения одной задачи на другую.",
            "Я могу длительное время сохранять сосредоточенность даже при монотонной работе.",
            "Прежде, чем приступить к выполнению задачи, я внимательно читаю или слушаю задание."
        },
        
        // Управление проектами
        new [] {
            "Я часто оказываюсь в роли организатора каких-то мероприятий в школе или среди друзей.",
            "За что бы я ни взялся/взялась, я стараюсь разработать план действий.",
            "Люблю общаться с новыми людьми обычно я сам принимаю финальное решение, даже если мои друзья в нем сомневаются.",
            "У меня получается заинтересовать других какой-то идеей.",
            "Чаще всего я довожу начатое дело до конца.",
            "Ищу наиболее оптимальное способы, когда делаю что-то, будь то уборка или подготовка домашнего задания.",
            "Как правило, мне удается склонить большинство своих товарищей к принятию ими Вашего мнения."
        },
        
        // Экологическое мышление
        new [] {
            "Я сдаю в переработку бумагу, батарейки, пластик.",
            "Я беспокоюсь о том, чтобы животные и окружающая среда не страдали от деятельности человека.",
            "Я хожу на субботники, участвую в экологических акциях.",
            "Я всегда слежу, чтобы вода не текла без надобности, выключаю свет, если он не нужен.",
            "Я не буду распечатывать материалы, если их можно прочитать в электронном виде.",
            "Я не беру в магазинах пластиковые пакеты, предпочитая тряпичные сумки и пакеты из бумаги.",
            "Я никогда не оставляю после себя мусор в кабинете, на улице."
        }
    };

    public static readonly int[] IntroQuizSkillsPromoted = {
        0, 0, 0, 0, 0, 0, 0,
        1, 1, 1, 1, 1, 1, 1,
        2, 2, 2, 2, 2, 2, 2,
        3, 3, 3, 3, 3, 3, 3,
        4, 4, 4, 4, 4, 4, 4,
        5, 5, 5, 5, 5, 5, 5
    };

    public const int IntroMAXPointsForSkill = 14;
    
    public static readonly string[][] TipsOfTheDay = {
        // Работа с людьми
        new [] {
            "Попробуйте эти кружки или хобби: Психологический тренинг, Актерские кружки, РДШ, КВН.",
            "Попробуйте поиграть в любые многопользовательские онлайн-игры.",
            "Попробуйте почитать Книги и журналы по психологии или Журналы для подростков.",
            "Может вам понравиться заниматься видеоблоггингом?"
        },
        
        // Межотраслевая коммуникация
        new [] {
            "Попробуйте эти кружки или хобби: Авиамодельный кружок, Судомодельный кружок, Автомодельный кружок.",
            "Попробуйте поиграть в игры жанра Квест.",
            "Попробуйте почитать книгу Грибоедова «Горе от ума».",
        },
        
        // Навыки художественного творчества
        new [] {
            "Попробуйте эти кружки или хобби: Художественная школа, Музыкальная школа, Танцевальные кружки," +
            " Кружки и мастер-классы народных ремесел, прикладного творчества, Компьютерная графика, дизайн.",
            "Попробуйте поиграть в такие игры, как: Диксит, Имаджинариум, Симуляторы, например, «Сам себе парикмахер», HappyColor.",
            "Попробуйте почитать Самоучитель по рисованию, Книги с репродукциями картин.",
            "Попробуйте посетить Экскурсии в художественные музеи, в т.ч. виртуальные."
        },
        
        // Работа в условиях неопределенности
        new [] {
            "Попробуйте эти кружки или хобби: Активный туризм, Дайвинг, Журналистика.",
            "Попробуйте поиграть в такие игры, как: Динамические игры, Heroes of Might and Magic, Клуб романтики, Коллективная игра «прятки».",
            "Попробуйте почитать Детективы, приключения, фантастика (например, «Марсианин»).",
        },
        
        // Управление проектами
        new [] {
            "Попробуйте эти кружки или хобби: Шахматы, Фото-видео-аудио-монтаж, Швейное мастерство.",
            "Попробуйте поиграть в такие игры, как: Игры планирования, Factorio, Heroes of Might and Magic, Настольные игры, например, Монополия.",
            "Попробуйте почитать Книги по тайм-менеджменту.",
            "Попробуйте посмотреть Youtube видео с экспериментами."
        },
        
        // Экологическое мышление
        new [] {
            "Попробуйте эти кружки или хобби: Экологический кружок, Обучение на волонтера.",
            "Попробуйте поиграть в такие игры, как: Цивилизация, Веселая ферма, Planescape: Torment, «Чистые игры» (игра по сбору мусора).",
            "Попробуйте поучаствовать в Экологических или Общественных акциях, например, «Зеленая белка», поищите Группы в контакте по теме."
        }
    };

    public static readonly string[] SkillsDescription = {
        // Работа с людьми
        "Успешный человек знает, что все люди разные, и умеет выстраивать взаимоотношения практически с каждым." +
        " Он умеет слушать и слышать, старается всегда понять, чем объясняются поступки людей и не торопится осуждать," +
        " давать оценки. Этот человек не испытывает затруднений при работе в команде: он может как организовать её," +
        " так и быть успешным участником команды, обе эти роли удаются ему одинаково хорошо.",
        
        // Межотраслевая коммуникация
        "Успешный человек умеет оценивать свои сильные и слабые стороны. Если в ходе его работы появляется вопрос," +
        " в котором человек не разбирается, человек знает, кто поможет с решением этой задачи. Человек с лёгкостью" +
        " подберёт доказательства того, что такая работа будет полезна обоим сторонам, что сотрудничество является" +
        " взаимовыгодным. Этот человек умеет делить объемные проекты подзадачи, часть которых делегирует." +
        " Иногда кажется, что он справится с любой задачей благодаря широкому кругу общения в разных сферах.",
        
        // Навыки художественного творчества
        "Успешный человек любит создавать новое, мыслит творчески, не боится выдвигать необычные идеи. У него всегда" +
        " готов неординарный способ решения проблемы, а ещё лучше - 10 способов. Кто-то предпочтёт развиваться в" +
        " области изобразительного искусства, кто-то найдёт себя в научных экспериментах и конструировании, кто-то" +
        " создаст музыкальный шедевр. Каждый талантлив, главное - раскрыть свои способности.",
        
        // Работа в условиях неопределенности
        "Успешный человек умеет адаптироваться к изменениям среды и практически не испытывает от частых перемен" +
        " стресса. Он обладает гибким мышлением и готов подстраиваться под новые задачи. Этот человек чётко планирует," +
        " но не грустит от изменений в планах, ведь это абсолютно нормальное явление. В ситуации недостатка данных" +
        " человек задаёт уточняющие вопросы или предлагает дальнейшие пути общих размышлений. Он успешно управляет" +
        " эмоциями и испытывает неподдельную радость от процесса решения нестандартных задач, даже в случае отсутствия быстрого результата.",
        
        // Управление проектами
        "Успешный человек умеет планировать время (что, конечно, повышает его личную эффективность)." +
        " Он реализует 99 своих идей из 100, свободно ориентируется в процессе продвижения от задуманного к результату." +
        " Этот человек точно знает, как разделить глобальную цель на выполнимые задачи, отследить качество выполнения" +
        " каждой из них и исправить ситуацию, если на одном из этапов что-то пошло не так. Иногда кажется," +
        " что в сутках этого человека 48 часов, он успевает в разы больше остальных.",
        
        // Экологическое мышление
        "Успешный человек задумывается о будущем планеты. Он точно знает, почему отказался от пластика," +
        " предпочёл раздельный сбор отходов и не выбрасывает батарейки в контейнер для мусора." +
        " Этот человек точно понимает, что всё в природе и обществе связано. Он осознанно учится взаимодействовать" +
        " со средой и готов учить других. Он просчитывает последствия своих действий и слов, старается улучшить мир вокруг."
    };
    
    // Extras

    public const float ScrollCharToSizeScale = 0.6f;

}
