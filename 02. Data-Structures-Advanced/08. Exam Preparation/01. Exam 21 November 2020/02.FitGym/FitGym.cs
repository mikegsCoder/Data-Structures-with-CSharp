namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> membersById;
        private Dictionary<int, Trainer> trainersById;
        private Dictionary<Trainer, HashSet<Member>> membersByTrainers;

        public FitGym()
        {
            this.membersById = new Dictionary<int, Member>();
            this.trainersById = new Dictionary<int, Trainer>();
            this.membersByTrainers = new Dictionary<Trainer, HashSet<Member>>();
        }

        public void AddMember(Member member)
        {
            if (this.Contains(member))
            {
                throw new ArgumentException();
            }

            this.membersById.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (this.Contains(trainer))
            {
                throw new ArgumentException();
            }

            this.trainersById.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!this.Contains(trainer) || member.Trainer != null)
            {
                throw new ArgumentException();
            }

            if (!this.Contains(member))
            {
                this.AddMember(member);
            }

            member.Trainer = trainer;

            if (!this.membersByTrainers.ContainsKey(trainer))
            {
                this.membersByTrainers.Add(trainer, new HashSet<Member>());
            }

            this.membersByTrainers[trainer].Add(member);
        }

        public bool Contains(Member member)
        {
            return this.membersById.ContainsKey(member.Id);
        }

        public bool Contains(Trainer trainer)
        {
            return this.trainersById.ContainsKey(trainer.Id);
        }

        public Trainer FireTrainer(int id)
        {
            if (!this.trainersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            Trainer toFire = this.trainersById[id];


            if (this.membersByTrainers.ContainsKey(toFire))
            {
                this.membersByTrainers[toFire].ToList().ForEach(x => { x.Trainer = null; });
                this.membersByTrainers.Remove(toFire);
            }

            this.trainersById.Remove(id);

            return toFire;
        }

        public Member RemoveMember(int id)
        {
            if (!this.membersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            Member toRemove = this.membersById[id];

            this.membersById.Remove(id);

            if (toRemove.Trainer != null)
            {
                this.membersByTrainers[toRemove.Trainer].Remove(toRemove);
            }

            toRemove.Trainer = null;

            return toRemove;
        }

        public int MemberCount => this.membersById.Count;

        public int TrainerCount => this.trainersById.Count;

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return this.membersById.Values
                .OrderBy(x => x.RegistrationDate)
                .ThenByDescending(x => x.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return this.trainersById.Values
                .OrderBy(x => x.Popularity);
        }

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            if (!this.membersByTrainers.ContainsKey(trainer))
            {
                return Enumerable.Empty<Member>();
            }

            return this.membersByTrainers[trainer]
                .OrderBy(x => x.RegistrationDate)
                .ThenBy(x => x.Name);
        }

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {
            return this.membersById.Values
                .Where(x => x.Trainer.Popularity >= lo && x.Trainer.Popularity <= hi)
                .OrderBy(x => x.Visits)
                .ThenBy(x => x.Name);
        }

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            return this.membersByTrainers
                .OrderBy(x => x.Value.Count)
                .ThenBy(x => x.Key.Popularity)
                .ToDictionary(k => k.Key, v => v.Value);
        }
    }
}