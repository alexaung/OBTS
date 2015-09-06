'use strict';
function dataServiceFactory() {
    var AppData = {}
    AppData.webapiurl = 'http://localhost:57448/api/'
     AppData.bus = {
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

    return { AppData: AppData };
}