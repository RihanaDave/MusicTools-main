namespace MusicTools;

public class  NoteOffQueue
{
    private readonly static int _queueLength = 100;
    private readonly Node[] _queue = new Node[_queueLength];
    private int _head = 0;
    private int _tail = 0;

    record struct Node
    {
        public int NextItem { get; set; }
        public int PreviousItem { get; set; }
        public Position EndsAt { get; set; }
        public Pitch Pitch { get; set; }
    }


    public NoteOffQueue()
    {
        for (var i = 0; i < _queueLength; i++)
        {
            _queue[i].PreviousItem = i - 1;
            _queue[i].NextItem = i + 1;
        }
        _queue[0].PreviousItem = (_queueLength - 1);
        _queue[_queueLength-1].NextItem = 0;
    }

    public void Push(Position position, Event evnt)
    {
        var endsAt = position + evnt.Duration;
        
        // When _head == _tail, the queue is empty.
        // _tail is always the first empty item in the queue.

        // If there is nothing in the queue, we just add it to the head, and move the tail forward one.
        if (_head == _tail)
        {
            _queue[_head].EndsAt = endsAt;
            _queue[_head].Pitch = evnt.Pitch;            
            _tail = _queue[_tail].NextItem;
            return;
        }

        // If its in front of the head, we need to move the head back one.
        if (endsAt <= _queue[_head].EndsAt)
        {
            _head = _queue[_head].PreviousItem;
            _queue[_head].EndsAt = endsAt;
            _queue[_head].Pitch = evnt.Pitch;
            return;
        }

        // If its behind the tail, we need to move the tail forward one.
        if (endsAt >= _queue[_queue[_tail].PreviousItem].EndsAt)
        {
            _queue[_tail].EndsAt = endsAt;
            _queue[_tail].Pitch = evnt.Pitch;
            _tail = _queue[_tail].NextItem;
            return;
        }

        // If its in the middle, we need to find the right place to insert it.
        if (endsAt > _queue[_head].EndsAt && endsAt < _queue[_tail].EndsAt)
        {
            var current = _head;
            while (position > _queue[current].EndsAt)
            {
                current = _queue[current].NextItem;
            }

            var previous = _queue[current].PreviousItem;
            _queue[previous].NextItem = _queue[_tail].NextItem;
            _queue[_tail].PreviousItem = previous;
            _queue[_tail].NextItem = current;
            _queue[current].PreviousItem = _tail;

            _queue[current].EndsAt = endsAt;
            _queue[current].Pitch = evnt.Pitch;
            return;
        }
    }

    public Pitch? Pop(Position position)
    {
        if(_head == _tail)
            return null;

        if (_queue[_head].EndsAt <= position)
        {
            var node = _queue[_head];
            _head = node.NextItem;
            return node.Pitch;
        }
       
        return null;
    }
}