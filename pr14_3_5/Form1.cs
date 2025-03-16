using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pr14_3_5
{
    public partial class Form1 : Form
    {
        private Queue<int> num = new Queue<int>();

        private Queue<string> young = new Queue<string>();
        private Queue<string> old = new Queue<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox1.Text, out n) && n > 0)
            {
                for (int i = 1; i <= n; i++)
                {
                    num.Enqueue(i);//Добавляет объект i в конец очереди
                }
                while (num.Count > 0)
                {
                    listBox1.Items.Add(num.Dequeue()); //Возвращает объект из начала вызывающей очереди, удаляя его из очереди
                }
            }
            else
            {
                MessageBox.Show("введите положительное целое число.", "Ошибка ввода", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("people.txt"))
            {
                string[] lines = File.ReadAllLines("people.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');
                    string[] nameParts = new string[parts.Length - 2];
                    Array.Copy(parts, nameParts, parts.Length - 2);
                    string fullName = string.Join(" ", nameParts);
                    int age;
                    if (int.TryParse(parts[parts.Length - 2], out age))
                    {
                        if (age < 40)
                        {
                            young.Enqueue(fullName + " " + age + " " + parts[parts.Length - 1]);//Добавляет объект в конец очереди
                        }
                        else
                        {
                            old.Enqueue(fullName + " " + age + " " + parts[parts.Length - 1]);//Добавляет объект в конец очереди
                        }
                    }
                    else
                    {
                        MessageBox.Show("некорректный ввод данных");
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter("people.txt"))
            {
                sw.WriteLine("люди младше 40 лет:");
                while (young.Count > 0)
                {
                    sw.WriteLine(young.Dequeue());//Возвращает объект из начала вызывающей очереди, удаляя его из очереди
                }
                sw.WriteLine("\nвсе остальные:");
                while (old.Count > 0)
                {
                    sw.WriteLine(old.Dequeue());//Возвращает объект из начала вызывающей очереди, удаляя его из очереди
                }
            }
            label2.Text = "результаты записаны в файл people.txt";
        }
    }
}
