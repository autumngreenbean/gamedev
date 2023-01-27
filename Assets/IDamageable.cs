using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public enum Alignment { Player, Enemy, DestructibleObject };

    public struct AlignmentMask
    {
        int mask;
        public AlignmentMask(IEnumerable<Alignment> alignments)
        {
            mask = 0;
            AddAlignments(alignments);
        }
        public AlignmentMask(params Alignment[] alignments)
        {
            mask = 0;
            AddAlignments(alignments);
        }
        public bool HasAlignment(Alignment alignment)
        {
            return (mask & (1 << ((int)alignment))) > 0;
        }
        public void AddAlignment(Alignment alignment)
        {
            mask = (mask | (1 << ((int)alignment)));
        }
        public void RemoveAlignment(Alignment alignment)
        {
            mask = mask & ~(1 << ((int)alignment));
        }
        public void AddAlignments(IEnumerable<Alignment> alignments)
        {
            foreach (Alignment alignment in alignments)
            {
                AddAlignment(alignment);
            }
        }
        public void AddAlignments(params Alignment[] alignments)
        {
            AddAlignments((IEnumerable<Alignment>)alignments);
        }
    }

    public abstract Alignment GetAlignment();

    public abstract void Damage(float amount, IDamageable source); 
    
}
