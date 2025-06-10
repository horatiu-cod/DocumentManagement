Creare  -> Status Creat->  Încărcare server
    verificare document -> owner este utilizatorul curent  -> este semnat de utilizatorul curent ( SignedBy, SignedOn), este editabil ( IsEditable = true, EditableBy - owner), alocat owner, Status Creat
    verificare FilesSystem -> verificat/creat folder 
                 MainRootFolder -> Cod SMIS > Adrese 
                                            >	Notificari -> Notificare
                                            >	Acte Aditionale -> Act Aditional
                                            >	Rapoarte Vizite
                                            >	Rapoarte Progres
                         verificare fișier -> 			         
                                            > 	nume real  -> codat Base64
                                            >	extensie fișier -> este admisibil
                                            >	mărime fișier -> e in limite ( nu e null sau prea mare)
                                            >	creare checksum

Transmitere -> 
	Verificare document -> nu este editabil ( IsEditable = False) nu poate fi descărcat, Status Transmis
	Verificare WorkFlow -> poate fi transmis


Alocare ->
	Verificare document -> nu este editabil ( IsEditable = False) nu poate fi descărcat
                                                    -> poate fi alocat responsabil ->  are rol adecvat  
                                                    -> Status Alocat -> este editabil ( IsEditable = true, EditableBy – utilizator curent),


Verificare -> Descărcat de pe server -> nume -> decodat Base64 
                                             -> nu e editabil ( IsEditable = false) 
                                             -> Status InEditare


Încărcare server
    verificare document -> verificator este utilizatorul curent  -> este semnat de utilizatorul curent ( SignedBy, SignedOn), este editabil ( IsEditable = true, EditableBy - utilizatorul curent  ), alocat utilizatorul curent  , Status Încărcat 

    verificare FilesSystem -> verificat folder 
                 MainRootFolder -> Cod SMIS > Adrese 
                                            >	Notificari -> Notificare
                                            >	Acte Aditionale -> Act Aditional
                                            >	Rapoarte Vizite
                                            >	Rapoarte Progres
                         verificare fișier -> 			         
                                            > 	nume real  -> codat Base64
                                            >	extensie fișier -> este admisibil
                                            >	mărime fișier -> e in limite ( nu e null sau prea mare)
                                            >	creare checksum


https://github.com/MuhammadrizoVositov/FileExplorer.API/tree/master