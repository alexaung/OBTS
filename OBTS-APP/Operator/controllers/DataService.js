'use strict';
function dataServiceFactory() {
    var Data = {}
     Data.busDto = {
        "BusId": "",
        "Brand": "",
        "BusType": "",
        "RegistrationNo": "",
        "PermitNumber": "",
        "PermitRenewDate": "",
        "InsurancePolicyNumber": "",
        "InsuranceCompany": "",
        "InsuranceValidFrom": "",
        "InsuranceValidTo": "",
        "VechiclePhoneNo": "",
        "DriverName": "",
        "Description": "",
        "Status": "",
        "OperatorId": ""
     }

     Data.carDto = {
         "BusId": "aaaa",
         "Brand": "",
         "BusType": "",
         "RegistrationNo": "",
         "PermitNumber": "",
         "PermitRenewDate": "",
         "InsurancePolicyNumber": "",
         "InsuranceCompany": "",
         "InsuranceValidFrom": "",
         "InsuranceValidTo": "",
         "VechiclePhoneNo": "",
         "DriverName": "",
         "Description": "",
         "Status": "",
         "OperatorId": ""
     }
    return { Data: Data };
}