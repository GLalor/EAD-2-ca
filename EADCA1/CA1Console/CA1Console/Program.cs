// X00122026
// Graham Lalor

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CA1Console
{
    class Client
    {
        static async Task Forum()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3365/");

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("**** Quirie 1 ****");
                    HttpResponseMessage response = await client.GetAsync("BPressure/bloodPressure/100/80");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        String bloodPressure = await response.Content.ReadAsAsync<String>();
                        Console.WriteLine("Blood Pressure for 100/80 is: " + bloodPressure);
              
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    Console.WriteLine("**** Quirie 2 ****");
                    response = await client.GetAsync("BPressure/bloodPressure/id/3");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var bloodPressures = await response.Content.ReadAsAsync<IEnumerable<BloodPressure>>();
                        Console.WriteLine("Blood Pressure reading for patient 3: ");
                        foreach (var bp in bloodPressures)
                        {
                            Console.WriteLine("Systolic = " + bp.Systolic + ", Diastolic = " + bp.Diastolic);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }


                    Console.WriteLine("**** Quirie 3 ****");
                    response = await client.GetAsync("/BPressure/bloodPressure/avgBP/id/2");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var bloodPressure = await response.Content.ReadAsAsync<BloodPressure>();
                        Console.WriteLine("Avgerage Blood Pressure reading for patient 2: ");
                        Console.WriteLine("Systolic = " + bloodPressure.Systolic + ", Diastolic = " + bloodPressure.Diastolic);

                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main()
        {
            Forum().Wait();
        }

    }
}
