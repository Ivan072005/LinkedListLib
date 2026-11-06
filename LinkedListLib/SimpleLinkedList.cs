using System;
using System.Collections.Generic;

// Интерфейс — это "договор": какие операции обязан уметь мой список.
// По условию задания его менять нельзя.
public interface ISimpleLinkedList<T>
{
    int Count { get; }
    void AddFirst(T value);
    void AddLast(T value);
    T RemoveFirst();
    bool Contains(T value);
}

// Односвязный список. Я представляю его как поезд из вагонов:
//
//   [10] → [20] → [30] → null
//    ↑                ↑
//  _head            _tail
//
// Каждый вагон знает только про СЛЕДУЮЩИЙ вагон (поэтому "односвязный").
// Готовые коллекции (List<T>, массивы и т.д.) не используются —
// элементы хранятся только в узлах, которые ссылаются друг на друга.
public class SimpleLinkedList<T> : ISimpleLinkedList<T>
{
    // Node — это один вагон поезда.
    // Value — груз в вагоне (само значение).
    // Next  — сцепка со следующим вагоном (null, если вагон последний).
    private class Node
    {
        public T Value { get; set; }
        public Node? Next { get; set; }

        public Node(T value) { Value = value; }
    }

    // Первый вагон поезда (голова). Если null — поезд пустой.
    private Node? _head;

    // Последний вагон поезда (хвост).
    // Я добавил это поле сам, чтобы AddLast работал быстро — O(1):
    // без него пришлось бы каждый раз идти от первого вагона до последнего.
    private Node? _tail;

    // Сколько сейчас вагонов в поезде.
    // Меняется только внутри класса (private set).
    public int Count { get; private set; }

    // Прицепить новый вагон В НАЧАЛО поезда. Сложность O(1).
    public void AddFirst(T value)
    {
        var node = new Node(value);   // создаём новый вагон
        node.Next = _head;            // цепляем его перед текущим первым вагоном
        _head = node;                 // теперь новый вагон — первый

        // Если поезд был пустым, то новый вагон одновременно и последний.
        if (_tail == null)
            _tail = node;

        Count++;
    }

    // Прицепить новый вагон В КОНЕЦ поезда. Сложность O(1) благодаря _tail.
    public void AddLast(T value)
    {
        var node = new Node(value);   // создаём новый вагон

        if (_head == null)
        {
            // Поезд пустой — новый вагон будет и первым, и последним.
            _head = node;
            _tail = node;
        }
        else
        {
            // Цепляем новый вагон за последним и делаем его новым хвостом.
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    // Отцепить ПЕРВЫЙ вагон и вернуть его груз. Сложность O(1).
    // Если поезд пустой — по условию задачи бросаем InvalidOperationException.
    public T RemoveFirst()
    {
        if (_head == null)
            throw new InvalidOperationException("List is empty");

        T value = _head.Value;        // запоминаем груз первого вагона
        _head = _head.Next;           // первым становится второй вагон

        // Если вагонов больше не осталось — хвост тоже должен стать null,
        // иначе _tail будет указывать на уже удалённый вагон
        // и следующий AddLast сломает список.
        if (_head == null)
            _tail = null;

        Count--;
        return value;
    }

    // Проверить, есть ли в поезде вагон с таким грузом. Сложность O(n).
    // Идём от первого вагона к последнему, пока не найдём совпадение.
    public bool Contains(T value)
    {
        var current = _head;          // начинаем с первого вагона

        while (current != null)       // идём, пока вагоны не закончатся
        {
            // EqualityComparer<T>.Default умеет корректно сравнивать
            // значения любого типа (int, string, null и т.д.).
            // Обычный == для обобщённого T не скомпилируется.
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
                return true;

            current = current.Next;   // переходим к следующему вагону
        }

        return false;                 // прошли весь поезд — не нашли
    }
}