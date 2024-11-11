using ClientServiceNamespace;
namespace ClinicDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientService clinic = new("http://localhost:5194", new HttpClient());
            ICollection<Client> clients = clinic.ClientGetAllAsync().Result;
            listViewClients.Items.Clear();
            foreach (Client client in clients)
            {
                ListViewItem item = new ListViewItem();
                item.Text = client.ClientId.ToString();
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = client.SurName });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = client.FirstName });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = client.Patronymic });
                listViewClients.Items.Add(item);
            }
        }
    }
}
