using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace assignment2_efaz
{

    public class zeroLayerException : Exception { };
    public abstract class Layer
    {
        private char name;
        private double thickness;

        public char Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }


        public void ModifyLayer(double Thickness, out double Remain)
        {
            Remain = (this.Thickness) * Thickness;
            this.Thickness = this.Thickness -Remain;
            
        }
        public bool notPerish() { return Thickness >= 0.5; }

        protected Layer(char str, double Thickness)
        {
            Name = str;
            this.Thickness = Thickness;
        }
        public void Simulate(ref List<Layer> layer, IAtmosphere atmosphere)
        {
            if (layer.Count<=0) { throw new zeroLayerException(); }
            int index = layer.IndexOf(this);
            Layer layers = Traverse(atmosphere);
            bool islayer = false;
            if (layer[index].Thickness < 0) { islayer = true; }
            if (layer[index].Thickness < 0.5)
            {
                for (int j = index + 1; j < layer.Count; ++j)
                {
                    if (layer[j].GetType() == layer[index].GetType())
                    {
                        layer[j].Thickness = layer[j].Thickness + Thickness + layers.Thickness;
                        islayer = true;
                        break;
                    }
                }
            }
            if(!islayer)
                for (int i = index + 1; i < layer.Count; i++)
                {
                    if (layer[i].GetType() == layers.GetType())
                    {
                        layer[i].Thickness = layer[i].Thickness +  layers.Thickness;
                        islayer = true;
                        break;
                    }
                }
            if(!islayer && layers.Thickness >= 0.5)
            {
                layer.Insert(layer.Count, layers);
            }
        }
        protected abstract Layer Traverse(IAtmosphere atmosphere);
    }

    public class Ozone : Layer
    {
        public Ozone(char str, double Thickness) : base(str, Thickness) { }
        protected override Layer Traverse(IAtmosphere atmosphere)
        {
            return atmosphere.ChangeOzone(this);
        }
    }

    public class Oxygen : Layer
    {
        public Oxygen(char str, double Thickness) : base(str, Thickness) { }
        protected override Layer Traverse(IAtmosphere atmosphere)
        {
            return atmosphere.ChangeOxygen(this);
        }
    }

    public class CarbonDioxide : Layer
    {
        public CarbonDioxide(char str, double Thickness) : base(str, Thickness) { }
        protected override Layer Traverse(IAtmosphere atmosphere)
        {
            return atmosphere.ChangeCarbon(this);
        }
    }
}