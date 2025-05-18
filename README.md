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

#### 1. Clone the Repository

   ```
   git clone https://github.com/Ehtisham3397/Radar_ERP_Calculator.git
   ```

#### 2. Install Requirements
   
Create a virtual environment (optional but recommended) and install dependencies:

   ```
   python -m venv venv
   source venv/bin/activate  # On Windows: venv\Scripts\activate
   pip install -r requirements.txt
   ```
#### 3. Set Up Environment Variables
   
Create a ```.env``` file in the root directory and add your API keys:

   ```
   Groq_API_Key=your_groq_api_key
   Google_API=your_google_cloud_translate_api_key
   ```
#### 4. Run the App
   
Start the Streamlit app:

   ```
   streamlit run app.py
   ```
The app will be available at  ```http://localhost:8501``` in your browser.

## 📖 Usage

#### 1. Script Generator Tab:

- Enter a topic (e.g., "Quantum Mechanics") and select a duration (1-30 minutes).

- Generate an educational script based on Wikipedia content.

- Optionally, create a video script with scene descriptions.

- Translate the script into a supported language (e.g., Urdu, Spanish).

- Download scripts as text files.

#### 2. Text-to-Speech Tab:

- Upload a TXT, PDF, or DOCX file.

- Convert the extracted text to speech with automatic language detection.

- Listen to the audio directly or download it as an MP3 file.

## 🌍 Supported Languages for Translation

The app supports translation into the following languages:

- Urdu, Punjabi, Pashto, Sindhi, Hindi, Arabic, Tamil, Telugu

- Chinese (Simplified & Traditional), Turkish, French, Spanish

- German, Italian, Russian, Japanese

## ⚠️ Notes

- Ensure your API keys are valid and have sufficient quotas for usage.

- The app requires an internet connection for API calls.

- For text-to-speech, supported file formats are TXT, PDF, and DOCX.

- RTL languages (e.g., Urdu, Arabic) are displayed with proper formatting.

## 📧 Contact

For questions or feedback, please open an issue on GitHub or contact [muhammadehtishamali2@example.com].

## 📜 License

This project is licensed under the Apache-2.0 License. See the LICENSE file for details.
