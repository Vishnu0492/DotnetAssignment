using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment6_Part3
{
    public class MainForm : Form
    {
        // Define controls
        private ListView listViewItems;
        private TextBox txtNewItem;
        private Button btnAdd;
        private Button btnRemove;

        public MainForm()
        {
            // Create and configure ListView
            listViewItems = new ListView
            {
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(260, 150),
                View = View.List, // Set the view to List
                FullRowSelect = true, // Allows full row selection
                HideSelection = false // Keeps the item highlighted when not focused
            };

            // Create and configure TextBox
            txtNewItem = new TextBox
            {
                Location = new System.Drawing.Point(12, 170),
                Size = new System.Drawing.Size(180, 20),
                PlaceholderText = "Enter new item" // Hint text (for .NET 5 and later)
            };

            // Create and configure Add Button
            btnAdd = new Button
            {
                Location = new System.Drawing.Point(200, 170),
                Size = new System.Drawing.Size(75, 23),
                Text = "Add"
            };
            btnAdd.Click += BtnAdd_Click;

            // Create and configure Remove Button
            btnRemove = new Button
            {
                Location = new System.Drawing.Point(12, 200),
                Size = new System.Drawing.Size(75, 23),
                Text = "Remove"
            };
            btnRemove.Click += BtnRemove_Click;

            // Add controls to the form
            Controls.Add(listViewItems);
            Controls.Add(txtNewItem);
            Controls.Add(btnAdd);
            Controls.Add(btnRemove);

            // Configure the form
            this.Text = "Win32 ListView Example";
            this.Size = new System.Drawing.Size(300, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Add button click event handler
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string itemText = txtNewItem.Text.Trim();
            if (!string.IsNullOrEmpty(itemText))
            {
                listViewItems.Items.Add(itemText); // Add item to ListView
                txtNewItem.Clear(); // Clear the TextBox
            }
            else
            {
                MessageBox.Show("Please enter a valid item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Remove button click event handler
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count > 0)
            {
                listViewItems.Items.Remove(listViewItems.SelectedItems[0]); // Remove selected item
            }
            else
            {
                MessageBox.Show("Please select an item to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // **Main method to run the application**
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

