This program targets the dot net framwork 4.7.1 and was built in Visual Studio Community 2017

To run it, you need to start both the web api service and the website. Right click on the solution, select properties, and select multiple startup and configure.

Notes:
1. There is no paging on the list view because the story didn't call for it (in real life, I would have been talking to the BA about that)
2. I set up a the website and api to use JWTs. Right now the secret is in the webconfig. In production, that would be in a TFS variable or some similar location and not exposed.
3. Also, if deployed, they should be only talking to each other over tls to prevent token interception. Additionally, the tokens generated are only good for 2 minutes to help prevent replay attacks.
4. Additionally, if I were setting this up for a real deploy, I'd only accept requests from the ip address of the website to the api. https://docs.microsoft.com/en-us/iis/configuration/system.webserver/security/ipsecurity/

Go to Server Explorer and run this query to see data directly:
 
SELECT * FROM Policy p
JOIN PrimaryInsured pi ON p.PolicyNumber = pi.PolicyNumber
JOIN RiskInsured ri ON ri.PolicyNumber = p.PolicyNumber
JOIN Address ria ON ria.AddressId = ri.AddressId
JOIN Address pia ON pia.AddressId = pi.AddressId
WHERE p.policyNumber = 
