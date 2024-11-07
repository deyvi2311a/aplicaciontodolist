namespace aplicaciontodolist.Views;

public partial class Calendario : ContentPage
{
    private int selectedMonth;
    private int selectedYear;
    private DateTime selectedDate;

    // Frases diarias
    private string[] dailyQuotes = {
        "'La vida es lo que sucede mientras est�s ocupado haciendo otros planes.'",
        "'No cuentes los d�as, haz que los d�as cuenten.'",
        "'La mejor forma de predecir el futuro es crearlo.'",
        "'Haz hoy lo que otros no quieren, haz ma�ana lo que otros no pueden.'",
        "'El �nico modo de hacer un gran trabajo es amar lo que haces.'"
    };

    public Calendario()
    {
        InitializeComponent();
        selectedMonth = DateTime.Now.Month;
        selectedYear = DateTime.Now.Year;
        selectedDate = DateTime.Now;
        UpdateMonthYearLabel();
        PopulateCalendar(selectedMonth, selectedYear);
        quoteLabel.Text = dailyQuotes[0]; // Muestra la primera frase por defecto
    }

    private void PopulateCalendar(int month, int year)
    {
        calendarGrid.Children.Clear();

        // Agregar d�as de la semana en la segunda fila
        string[] daysOfWeek = { "L", "M", "X", "J", "V", "S", "D" };
        for (int i = 0; i < 7; i++)
        {
            Label dayLabel = new Label
            {
                Text = daysOfWeek[i],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black
            };
            calendarGrid.Children.Add(dayLabel);
            Grid.SetColumn(dayLabel, i);
            Grid.SetRow(dayLabel, 1); // Coloca en la segunda fila
        }

        // Fecha inicial del mes
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);

        // D�a de la semana en que comienza el mes (0 = Domingo, 1 = Lunes, ..., 6 = S�bado)
        int startDay = (int)firstDayOfMonth.DayOfWeek;

        // Crear botones para cada d�a
        for (int day = 1; day <= daysInMonth; day++)
        {
            Button dayButton = new Button
            {
                Text = day.ToString(),
                BackgroundColor = (day == selectedDate.Day && month == selectedDate.Month && year == selectedDate.Year)
                                  ? Colors.LightBlue
                                  : Colors.Black,
                TextColor = Colors.White
            };
            dayButton.Clicked += OnDaySelected;

            // Agregar el bot�n a la cuadr�cula
            calendarGrid.Children.Add(dayButton);

            // Calcular la fila y columna para el d�a
            int row = (startDay + day - 1) / 7 + 2; // +2 para dejar la fila de los d�as de la semana y la fila de mes/a�o
            int column = (startDay + day - 1) % 7;
            Grid.SetColumn(dayButton, column);
            Grid.SetRow(dayButton, row);
        }

        // A�adir filas adicionales si es necesario
        int totalRowsNeeded = (daysInMonth + startDay + 6) / 7; // Redondea hacia arriba
        for (int i = 0; i < totalRowsNeeded; i++)
        {
            calendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }
    }

    private void OnDaySelected(object sender, EventArgs e)
    {
        Button selectedButton = (Button)sender;
        int day = int.Parse(selectedButton.Text);
        selectedDate = new DateTime(selectedYear, selectedMonth, day);

        // Actualiza el calendario para mostrar el d�a seleccionado
        PopulateCalendar(selectedMonth, selectedYear);

        // Cambia la frase al azar al seleccionar un d�a
        Random random = new Random();
        int index = random.Next(dailyQuotes.Length);
        quoteLabel.Text = dailyQuotes[index]; // Selecciona una frase aleatoria

       // DisplayAlert("Fecha Seleccionada", $"Has seleccionado: {selectedDate.ToString("D")}", "OK");
    }

    public void OnNextMonth(object sender, EventArgs e)
    {
        if (selectedMonth == 12)
        {
            selectedMonth = 1;
            selectedYear++;
        }
        else
        {
            selectedMonth++;
        }
        UpdateMonthYearLabel();
        PopulateCalendar(selectedMonth, selectedYear);
    }

    public void OnPreviousMonth(object sender, EventArgs e)
    {
        if (selectedMonth == 1)
        {
            selectedMonth = 12;
            selectedYear--;
        }
        else
        {
            selectedMonth--;
        }
        UpdateMonthYearLabel();
        PopulateCalendar(selectedMonth, selectedYear);
    }

    private void UpdateMonthYearLabel()
    {
        monthYearLabel.Text = $"{new DateTime(selectedYear, selectedMonth, 1):MMMM yyyy}";
    }
}
