namespace SuporteBalanceados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string entrada = @"((({}{}))()";
            var stack = new Stack<char>();

            foreach (var item in entrada)
            {
                if (item == '(' || item == '[' || item == '{')
                {
                    stack.Push(item);
                }
                else
                {
                    switch (item)
                    {
                        case ')':
                            if (stack.Peek() == '(') stack.Pop();
                            break;
                        case ']':
                            if (stack.Peek() == '[') stack.Pop();
                            break;
                        case '}':
                            if (stack.Peek() == '{') stack.Pop();
                            break;

                        default: break;
                    }
                }
            }

            if (stack.Count == 0)
            {
                Console.WriteLine(entrada + " é válido");
            }
            else
            {
                Console.WriteLine(entrada + " não é válido");
            }
        }
    }
}