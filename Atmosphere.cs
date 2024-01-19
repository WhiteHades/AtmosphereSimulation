using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace assignment2_efaz
{

    public interface IAtmosphere
    {
        Layer ChangeOzone(Ozone oz);
        Layer ChangeOxygen(Oxygen ox);
        Layer ChangeCarbon(CarbonDioxide ca);

        private static IAtmosphere instance = null;
        public static IAtmosphere Instance()
        {
            if (instance == null)
            {
                instance = new Thunderstorm();
            }
            return instance;
        }
    }

    public class Thunderstorm : IAtmosphere
    {
        public Layer ChangeOzone(Ozone oz)
        { return new Ozone('Z', 0); }

        public Layer ChangeOxygen(Oxygen ox)
        {
            ox.ModifyLayer(0.5, out double Remain);
            return new Ozone('Z', Remain);
        }

        public Layer ChangeCarbon(CarbonDioxide ca)
        { return new CarbonDioxide('C', 0); }

        public static Thunderstorm Instance2
        {
            get;
        } = new Thunderstorm();

        private static Thunderstorm instance = null;
        public static Thunderstorm Instance()
        {
            if (instance == null)
            {
                instance = new Thunderstorm();
            }
            return instance;
        }
    }

    public class Sunshine : IAtmosphere
    {
        public Layer ChangeOzone(Ozone oz)
        { return new Ozone('Z', 0); }

        public Layer ChangeOxygen(Oxygen ox)
        {
            ox.ModifyLayer(0.05, out double Remain);
            return new Ozone('Z', Remain);
        }

        public Layer ChangeCarbon(CarbonDioxide ca)
        {
            ca.ModifyLayer(0.05, out double Remain);
            return new Oxygen('X', Remain);
        }

        public static Sunshine Instance2
        {
            get;
        } = new Sunshine();

        private static Sunshine instance = null;
        public static Sunshine Instance()
        {
            if (instance == null)
            {
                instance = new Sunshine();
            }
            return instance;
        }
    }

    public class Other : IAtmosphere
    {
        public Layer ChangeOzone(Ozone oz)
        {
            oz.ModifyLayer(0.05, out double Remain);
            return new Oxygen('X', Remain);
        }

        public Layer ChangeOxygen(Oxygen ox)
        {
            ox.ModifyLayer(0.1, out double Remain);
            return new CarbonDioxide('C', Remain);
        }

        public Layer ChangeCarbon(CarbonDioxide ca)
        { return new CarbonDioxide('C', 0); }

        public static Other Instance2
        {
            get;
        } = new Other();

        private static Other instance = null;
        public static Other Instance()
        {
            if (instance == null)
            {
                instance = new Other();
            }
            return instance;
        }
    }
}