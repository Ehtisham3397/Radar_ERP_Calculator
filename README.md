# Radar ERP Calculator

A Windows-based application built with WPF (.NET) to calculate the Effective Radiated Power (ERP) and Antenna Gain for radar systems. It also calculates Jammer required power for a particular Radar at user defined distance. It also claculates Radar range based on target RCS. 
This tool helps users input radar parameters and visualize the results in an intuitive interface.

## Overview 
The Radar ERP Calculator allows users to:

 - Input radar details such as name, peak power, operating frequency, antenna aperture, and efficiency.
 - Compute Antenna Gain (dB) and Total ERP (dBm) based on the provided parameters.
 - Display results in a table for easy reference and analysis.
 - Compute Jammer power to effectively jam a Radar.
 - Compute Jammer range based on target RCS.

This project is developed using XAML for the user interface and C# for the backend logic.

## ✨ Features
 - Modern UI with customizable styles and themes.
 - Real-time calculation of Antenna Gain and ERP.
 - Calculates Jammer required Power to Jam a particular Radar at defined Distance.
 - Calculates Jammer Range based on target RCS.
 - Data grid to store and review multiple radar parameter sets.
 - Input validation for numerical fields.

## 📋 Prerequisites
**Windows OS** (Windows 10 or later recommended).

**.NET Framework 4.8** or later (or .NET Core 3.1+ depending on the build).

**Visual Studio 2019/2022** (for development and building the project).


## 📋 Prerequisites

To run this application locally, ensure you have the following:

   - Python 3.8 or higher

   - A Groq API key (set as ```Groq_API_Key``` in a ```.env``` file)

   - A Google Cloud Translate API key (set as ```Google_API``` in a ```.env``` file)

   - Required Python packages listed in ```requirements.txt```

## 🚀 Installation

1. Clone the Repository:

   ```
   git clone https://github.com/Ehtisham3397/Radar_ERP_Calculator.git
   ```
   
2. Open the solution file ```(Radar_ERP_Calculator.sln)``` in Visual Studio.
3. Restore NuGet packages if prompted.
4. Build the solution (Build > Build Solution).
5. Run the application (F5 or Debug > Start Debugging).
   
## 📖 Usage
 1. Launch the application.
 2. Navigate to the "Radar ERP" tab.
 3. Enter the radar name, peak power (kW), operating frequency (GHz), antenna aperture, and efficiency.
 4. Click the "Calculate" button to compute and display the Antenna Gain and Total ERP.
 5. View the results in the data grid below.
 6. Use the "Delete" button (if visible) to clear the current data set.
 7. In second Tab you can select previous stored Radar or can enter Parameters for a new one and then calculate Required Power to Jam that Radar in SIJ/SOJ type jammer or SPJ type jammer.
 8. In third Tab you can again select previous stored Radar or can enter parameters for a new one to calculate Radar Range for a give target RCS.


## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch ( ```git checkout -b feature-branch ```).
3. Make your changes and commit them ( ```git commit -m "Description of changes" ```).
4. Push to the branch ( ```git push origin feature-branch ```).
5. Open a pull request.

Please ensure your code follows the existing style and includes appropriate comments.

## ⚠️ Notes

- Ensure your API keys are valid and have sufficient quotas for usage.

- The app requires an internet connection for API calls.

- For text-to-speech, supported file formats are TXT, PDF, and DOCX.

- RTL languages (e.g., Urdu, Arabic) are displayed with proper formatting.

## 📧 Contact

For questions or support, please open an issue on the GitHub repository or contact the maintainer at [muhammadehtishamali2@example.com].

## 📜 License

This project is licensed under the Apache-2.0 License. See the LICENSE file for details.
