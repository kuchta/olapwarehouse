namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information of an Olap cube.
    /// </summary>
    public class OlapCubeInformation
    {
        /// <summary>
        /// Holds the type of the cube.
        /// </summary>
        private OlapCubeType _cubeType;

        /// <summary>
        /// Holds a timestamp of when the cube was updated the last time.
        /// </summary>
        private int _lastUpdate;

        /// <summary>
        /// Number of base values.
        /// </summary>
        private int _baseValueCount;

        /// <summary>
        /// Number of calculated values.
        /// </summary>
        private int _calculatedValueCount;

        /// <summary>
        /// Initializes a new instance of the OlapCubeInformation class.
        /// </summary>
        /// <param name="cubeType">The type of the cube.</param>
        /// <param name="lastUpdate">When the cube was updated the last time.</param>
        /// <param name="baseValueCount">The number of base values.</param>
        /// <param name="calculatedValueCount">The number of calculated values.</param>
        public OlapCubeInformation(OlapCubeType cubeType, int lastUpdate, int baseValueCount, int calculatedValueCount)
        {
            _cubeType = cubeType;
            _lastUpdate = lastUpdate;
            _baseValueCount = baseValueCount;
            _calculatedValueCount = calculatedValueCount;
        }

        /// <summary>
        /// Gets the cube type.
        /// </summary>
        public OlapCubeType CubeType
        {
            get
            {
                return _cubeType;
            }
        }

        /// <summary>
        /// Gets a timestamp when the cube was updated the last time.
        /// </summary>
        public int LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
        }

        /// <summary>
        /// Gets the number of base values.
        /// </summary>
        public int BaseValueCount
        {
            get
            {
                return _baseValueCount;
            }
        }

        /// <summary>
        /// Gets the number of calculated values.
        /// </summary>
        public int CalculatedValueCount
        {
            get
            {
                return _calculatedValueCount;
            }
        }

        /// <summary>
        /// Creates the string that represents the data of this class.
        /// </summary>
        /// <returns>A string that represents the data of this class.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            result.Append("CubeType=");
            result.Append(_cubeType);
            result.Append(", LastUpdate=");
            result.Append(_lastUpdate);
            result.Append(", BaseValueCount=");
            result.Append(_baseValueCount);
            result.Append(", CalculatedValueCount=");
            result.Append(_calculatedValueCount);
            return result.ToString();
        }
    }
}
