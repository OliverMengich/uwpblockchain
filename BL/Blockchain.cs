﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace BL
{
    public class Blockchain
    {
        private readonly int _proofOfWorkDifficulty;
        private readonly double _miningReward;
        private List<Transaction> _pendingTransactions;
        public List<Block> Chain { get; set; }
        public Blockchain(int proofOfWorkDifficulty, int miningReward)
        {
            _proofOfWorkDifficulty = proofOfWorkDifficulty;
            _miningReward = miningReward;
            _pendingTransactions = new List<Transaction>();
            Chain = new List<Block> { };
        }
        private Block CreateGenesisBlock()
        {
            var transactions = new List<Transaction> { new Transaction("Oliver", "Kipkemei", 55.7) };
            return new Block(DateTime.Now, transactions,"0");
        }
        public void CreateTransaction(Transaction transaction)
        {
            _pendingTransactions.Add(transaction);
        }
        public void MineBlock(string minerAddress)
        {
            Transaction minerRewardTransaction = new Transaction(null, minerAddress, _miningReward);
            _pendingTransactions.Add(minerRewardTransaction);
            Block block = new Block(DateTime.Now, _pendingTransactions);
            block.MineBlock(_proofOfWorkDifficulty);
            block.PreviousHash = Chain.Last().Hash;
            Chain.Add(block);
            _pendingTransactions = new List<Transaction>();
        }
        // now to validate a chain
        public bool IsValidChain()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block previousBlock = Chain[i - 1];
                Block currentBlock = Chain[i];
                if (currentBlock.Hash != currentBlock.CreateHash())
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true; 
        }
        public double GetBalance(string address)
        {
            double balance = 0;
            foreach (Block block in Chain)
            {
                foreach (Transaction transaction in block.Transactions)
                {
                    if (transaction.From == address)
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.To == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }
    }
}
