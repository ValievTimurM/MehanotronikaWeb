﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Helpers
{
  public static class EntityHelper
  {
    public static IList<string> GetCarModelNames()
        => new List<string>()
        {
             "Лада Гранта",
             "Лада Веста",
             "Тойота Камри",
             "Хонда Цивик",
             "Мазда 3",
             "Форд Фокус",
             "Ниссан Альмера",
             "Вольксваген Гольф",
             "БМВ 3 Серии",
             "Мерседес C-Класс",
             "Ауди A4",
             "Лексус IS",
             "Инфинити Q50",
             "Киа Селтос",
             "Хёндай Элантра",
             "Мицубиси Лансер",
             "Субару Импреза",
             "Скода Октавия",
             "Фольксваген Пассат",
             "Опель Астра",
             "Пежо 308",
             "Рено Меган",
             "Ситроен C4",
             "Фиат Tipo",
             "Киа Рио",
             "Хёндай i30",
             "Мазда 6",
             "Тойота Королла",
             "Ниссан Пульсар",
             "Форд Мондео"
        };

    public static IList<string> GetDriversFio()
    {
      return new List<string>
            {
             "Иванов Иван Иванович",
             "Петров Петр Петрович",
             "Сидоров Сергей Сергеевич",
             "Кузнецов Дмитрий Дмитриевич",
             "Смирнов Александр Александрович",
             "Николаев Николай Николаевич",
             "Зайцев Михаил Михайлович",
             "Лебедев Андрей Андреевич",
             "Попов Виктор Викторович",
             "Михайлов Евгений Евгеньевич",
             "Федоров Олег Олегович",
             "Волков Сергей Сергеевич",
             "Алексеев Дмитрий Дмитриевич",
             "Ковалев Александр Александрович",
             "Гордеев Иван Иванович",
             "Романов Михаил Михайлович",
             "Васильев Андрей Андреевич",
             "Тихонов Сергей Сергеевич",
             "Морозов Дмитрий Дмитриевич",
             "Савельев Александр Александрович",
             "Данилов Евгений Евгеньевич",
             "Кузьмин Михаил Михайлович",
             "Филиппов Олег Олегович",
             "Белов Сергей Сергеевич",
             "Комаров Андрей Андреевич",
             "Орлов Дмитрий Дмитриевич",
             "Новиков Александр Александрович",
             "Дмитриев Михаил Михайлович",
             "Крылов Евгений Евгеньевич",
             "Матвеев Сергей Сергеевич"
            };
    }
  }
}
