# ![image](https://github.com/user-attachments/assets/93d8bc4f-da60-48b2-aa23-a2e6e5e9bf78)
Radar ERP Calculator

A Windows-based application built with WPF (.NET) to calculate the Effective Radiated Power (ERP) and Antenna Gain for radar systems. It also calculates Jammer required power for a particular Radar at user defined distance. It also claculates Radar range based on target RCS. 
This tool helps users input radar parameters and visualize the results in an intuitive interface.

## ✨ Features
 - Modern UI with customizable styles and themes.

 - Real-time calculation of Antenna Gain and ERP.

 - Calculates Jammer required Power to Jam a particular Radar at defined Distance.

 - Calculates Jammer Range based on target RCS.

 - Data grid to store and review multiple radar parameter sets.

 - Input validation for numerical fields.

## 🛠️ Technologies Used
**Streamlit:** For building the interactive web interface.

**Groq API:** For generating educational and video scripts using AI.

**Google Cloud Translate API:** For translating scripts into multiple languages.

**Wikipedia API:** For fetching factual content to base scripts on.

**gTTS (Google Text-to-Speech):** For converting text to speech.

**PyPDF2 & python-docx:** For extracting text from PDF and DOCX files.

**Langdetect:** For automatic language detection in uploaded files.

**Tempfile:** For handling temporary file storage for downloads.

## 📋 Prerequisites

To run this application locally, ensure you have the following:

   - Python 3.8 or higher

   - A Groq API key (set as ```Groq_API_Key``` in a ```.env``` file)

   - A Google Cloud Translate API key (set as ```Google_API``` in a ```.env``` file)

   - Required Python packages listed in ```requirements.txt```

## 🚀 How to run the App Locally

#### 1. Clone the Repository

   ```
   git clone https://github.com/your-username/your-repo-name.git
   cd your-repo-name
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
