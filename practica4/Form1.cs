using System;
using System.Drawing;
using System.Windows.Forms;

namespace ListViewExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadData();
        }

        private void InitializeCustomComponents()
        {
            // Создание ImageList для хранения изображений
            listView1.SmallImageList = new ImageList();
            listView1.SmallImageList.ImageSize = new Size(50, 50);

            // Добавление столбцов в ListView
            listView1.Columns.Add("Изображение", 100);
            listView1.Columns.Add("Имя", 100);
            listView1.Columns.Add("Фамилия", 100);

            // Кнопка для добавления нового элемента
            button1.Text = "Добавить";
            button1.Click += AddNewItem;

            // Кнопка для выбора изображения
            Button btnAddImage = new Button
            {
                Location = new Point(400, 300),
                Text = "Выбрать изображение"
            };
            btnAddImage.Click += BtnAddImage_Click;
            Controls.Add(btnAddImage);
        }

        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загрузка выбранного изображения
                    Image img = Image.FromFile(openFileDialog.FileName);
                    listView1.SmallImageList.Images.Clear();
                    listView1.SmallImageList.Images.Add(img);
                }
            }
        }

        private void LoadData()
        {
            // Добавление начальных данных
            AddItem("Иван", "Иванов", "cat1.jpg");
        }

        private void AddNewItem(object sender, EventArgs e)
        {
            // Получение данных из текстовых полей
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Введите имя и фамилию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Добавление нового элемента в ListView
            AddItem(firstName, lastName, "cat.jpg"); // Используем стандартное изображение, можно изменить
        }

        private void AddItem(string firstName, string lastName, string imagePath)
        {
            // Загружаем изображение
            Image image;
            try
            {
                image = Image.FromFile(imagePath);
            }
            catch (Exception)
            {
                image = new Bitmap(50, 50);
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(Color.White);
                }
            }

            // Добавляем изображение в ImageList
            int imageIndex = listView1.SmallImageList.Images.Count;
            listView1.SmallImageList.Images.Add(image);

            // Создание элемента для ListView
            ListViewItem item = new ListViewItem("", imageIndex)
            {
                SubItems = { firstName, lastName }
            };

            // Добавление элемента в ListView
            listView1.Items.Add(item);
        }

        
    }
}
