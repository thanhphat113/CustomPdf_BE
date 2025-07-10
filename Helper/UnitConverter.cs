using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.Helper
{
    public static class UnitConverter
    {
        public static float PxToMM(double px) => (float)(px / 3.78);

        public static float MmToPt(double mm) => (float)(mm * 2.8346);
        public static float PxToPoints(double px) => (float)px * 0.75f;
    }
}