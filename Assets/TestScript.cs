using System;
using System.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thirdweb;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public TextAsset sampleData;

    [System.Serializable]
    private class DataDTO
    {
        public string abi;
        public string functionName;
        public string contractAddress;
        public string decodedInput;
        public OverridesDTO overrides;
    }

    [System.Serializable]
    private class OverridesDTO
    {
        public string to;
        public string from;
        public string value;
        public string data;
    }
    // Start is called before the first frame update
    private async void Start()
    {
        Debug.Log("Starting test");
        var trade =  JsonConvert.DeserializeObject<DataDTO>(sampleData.text);
        Debug.Log("Deserialized sample");
        Debug.Log("Will coneect to chain id: "+ThirdwebManager.Instance.chain);
        var chainData = GetChainData();
        Debug.Log("Got chain data");
        var wal = ThirdwebManager.Instance.SDK.wallet;
        var conn = new WalletConnection(WalletProvider.Metamask, int.Parse(chainData.chainId));
        string address = await wal.Connect(conn);
        Debug.Log("Connected to wallet "+address);
        Thirdweb.Contract con = new Thirdweb.Contract(chainData.chainId, trade.contractAddress, trade.abi);

        var args = JArray.Parse(trade.decodedInput);
        var args0 = new BigInteger(Convert.ToInt32(args[0]["hex"].ToString(), 16));
        var args1 = args[1][0].ToString(); //byte array in "0x..." hex form
        Debug.Log($"Invoking {trade.functionName} on the contract");
        Debug.Log($"Argument 0(uint256): {args0}");
        Debug.Log($"Argument 1(bytes[]): {args1}");
        TransactionResult txRes =  await con.Write(trade.functionName, new TransactionRequest {
            from = address,
            value = Convert.ToInt32(trade.overrides.value, 16).ToString()
        }, args0, args1);

        Debug.Log("got hash "+txRes.receipt.transactionHash);   
    }
    
    public ChainData GetChainData() {
        return ThirdwebManager.Instance.supportedChains.Find((x)=>x.identifier == ThirdwebManager.Instance.chain);
    }
}
