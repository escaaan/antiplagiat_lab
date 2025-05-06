﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace antiplagiat_lab
{
    public partial class EditGroupForm : Form
    {
        private List<Group> groups;

        public EditGroupForm(List<Group> groups)
        {
            InitializeComponent();
            this.groups = groups;
            FillGroupComboBox();
        }

        private void FillGroupComboBox()
        {
            comboBox_Groups.Items.Clear();
            foreach (var group in groups)
            {
                comboBox_Groups.Items.Add(group.Name);
            }
        }

        private void comboBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_Students.Items.Clear();
            var selectedGroup = groups.FirstOrDefault(g => g.Name == comboBox_Groups.SelectedItem.ToString());
            if (selectedGroup != null)
            {
                foreach (var student in selectedGroup.Students)
                {
                    listBox_Students.Items.Add(student.Name);
                }
            }
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_NewStudent.Text))
            {
                listBox_Students.Items.Add(textBox_NewStudent.Text);
                textBox_NewStudent.Clear();
            }
        }

        private void buttonRemoveStudent_Click(object sender, EventArgs e)
        {
            if (listBox_Students.SelectedItem != null)
            {
                listBox_Students.Items.Remove(listBox_Students.SelectedItem);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBox_Groups.SelectedItem != null)
            {
                var selectedGroup = groups.FirstOrDefault(g => g.Name == comboBox_Groups.SelectedItem.ToString());

                if (selectedGroup != null)
                {
                    var updatedStudents = new List<Student>();

                    foreach (string studentName in listBox_Students.Items)
                    {
                        var existingStudent = selectedGroup.Students.FirstOrDefault(s => s.Name == studentName);

                        if (existingStudent != null)
                        {
                            updatedStudents.Add(existingStudent);
                        }
                        else
                        {
                            updatedStudents.Add(new Student { Name = studentName });
                        }
                    }

                    selectedGroup.Students = updatedStudents;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите группу.");
            }
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
