using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
	/// <summary>
	/// Currency class
	/// </summary>
	public class Devise
    {
		private int id;

        /// <summary>
        /// Currency's id
        /// </summary>
        public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[Required]
		private string nomDevise;

        /// <summary>
        /// Currency's name
        /// </summary>
        public string NomDevise
		{
			get { return nomDevise; }
			set { nomDevise = value; }
		}

		private double taux;

        /// <summary>
        /// Currency's rate
        /// </summary>
        public double  Taux
		{
			get { return taux; }
			set { taux = value; }
		}

        /// <summary>
		/// Currency's default constructor
		/// </summary>
		public Devise()
		{ }

        /// <summary>
		/// Create a currency
		/// </summary>
		/// <param name="id"></param>
		/// <param name="nomDevise"></param>
		/// <param name="taux"></param>
		public Devise(int id, string nomDevise, double taux)
        {
            this.Id = id;
            this.NomDevise = nomDevise;
            this.Taux = taux;
        }

        /// <summary>
        /// Equals function
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Devise devise &&
                   this.Id == devise.Id &&
                   this.NomDevise == devise.NomDevise &&
                   this.Taux == devise.Taux;
        }

        /// <summary>
        /// GetHashCode function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.NomDevise, this.Taux);
        }
    }
}
