
namespace Calculator_Radar_Params.UserParams
{
    public class UserParameters
    {
        public double Radar_ERP {  get; set; }
        public double Radar_Operating_Freq { get; set; }
        public double Radar_Antenna_Gain { get; set; }
        public double Radar_Antenna_SLL { get; set; }
        public double Radar_Losses { get; set; }
        public double Intruder_RCS  { get; set; }
        public double Intruder_Range { get; set; }
        public double Jammer_RCS { get; set; }
        public double Jammer_Range_SPJ { get; set;}
        public double Jamming_to_Signal_Ratio { get; set; }
        public double Jammer_Range_Support { get; set; }
        public double Jammer_Losses { get; set; }
        public double AntennaGain_SideLobe { get; set; }
    }
}
