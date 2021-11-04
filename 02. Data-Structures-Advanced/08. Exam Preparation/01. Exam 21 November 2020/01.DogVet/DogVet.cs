namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById;
        private Dictionary<string, Owner> ownersById;
        private Dictionary<Owner, HashSet<Dog>> dogsByOwner;
        private Dictionary<Breed, HashSet<Dog>> dogsByBreed;
       
        public DogVet()
        {
            this.dogsById = new Dictionary<string, Dog>();
            this.ownersById = new Dictionary<string, Owner>();
            this.dogsByOwner = new Dictionary<Owner, HashSet<Dog>>();
            this.dogsByBreed = new Dictionary<Breed, HashSet<Dog>>();            
        }

        public int Size => this.dogsById.Count();

        public void AddDog(Dog dog, Owner owner)
        {
            if (this.Contains(dog))
            {
                throw new ArgumentException();
            }

            if (this.dogsByOwner.ContainsKey(owner) && this.dogsByOwner[owner].Any(x => x.Name == dog.Name))
            {
                throw new ArgumentException();
            }

            dog.Owner = owner;

            this.dogsById.Add(dog.Id, dog);

            if (!this.ownersById.ContainsKey(owner.Id))
            {
                this.ownersById.Add(owner.Id, owner);
            }

            if (!this.dogsByOwner.ContainsKey(owner))
            {
                this.dogsByOwner.Add(owner, new HashSet<Dog>());
            }

            this.dogsByOwner[owner].Add(dog);

            if (!this.dogsByBreed.ContainsKey(dog.Breed))
            {
                this.dogsByBreed.Add(dog.Breed, new HashSet<Dog>());
            }

            this.dogsByBreed[dog.Breed].Add(dog);
        }

        public bool Contains(Dog dog)
        {
            return this.dogsById.ContainsKey(dog.Id);
        }

        public Dog GetDog(string name, string ownerId)
        {
            if (!this.ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            var owner = this.ownersById[ownerId];

            if (!this.dogsByOwner[owner].Any(x => x.Name == name))
            {
                throw new ArgumentException();
            }

            return this.dogsByOwner[owner].FirstOrDefault(x => x.Name == name);
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            Dog toRemove = this.GetDog(name, ownerId);
            Owner owner = this.ownersById[ownerId];

            this.dogsById.Remove(toRemove.Id);
            this.dogsByOwner[owner].Remove(toRemove);
            this.dogsByBreed[toRemove.Breed].Remove(toRemove);
            
            return toRemove;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!this.ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            Owner owner = this.ownersById[ownerId];

            return this.dogsByOwner[owner];
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            if (!this.dogsByBreed.ContainsKey(breed))
            {
                throw new ArgumentException();
            }

            return this.dogsByBreed[breed];
        }

        public void Vaccinate(string name, string ownerId)
        {
            Dog toVaccinate = this.GetDog(name, ownerId);

            toVaccinate.Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            Dog dog = this.GetDog(oldName, ownerId);
            Owner owner = this.ownersById[ownerId];

            if (this.dogsByOwner[owner].Any(x => x.Name == newName))
            {
                throw new ArgumentException();
            }

            dog.Name = newName;
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            if (!this.dogsById.Values.Any(x => x.Age == age))
            {
                throw new ArgumentException();
            }

            return this.dogsById.Values.Where(x => x.Age == age);
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            return this.dogsById.Values.Where(x => x.Age >= lo && x.Age <= hi);            
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            return this.dogsById.Values.OrderBy(x => x.Age).ThenBy(x => x.Name).ThenBy(x => x.Owner.Name);            
        }
    }
}