		
			function UpdateSelectiveValues(ObjValue, TipoMicId, CodigoMacId, CuencaId) {
			    var TipoMic;
			    var CodigoMac;
			    var Cuenca;


			    switch ($('#' + ObjValue.id + ' option:selected').text()) {
			        case "MCA519":
			        case "MCA518":
			            TipoMic = "MIC";
			            CodigoMac = "MACCEN0190";
			            Cuenca = "Maldonado";
			            break;
			        case "MCE524":
			            TipoMic = "MIC";
			            CodigoMac = "MACCEN0590";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA018":
			        case "MFL023":
			            TipoMic = "MID";
			            CodigoMac = "MACCAB2220";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA024":
			        case "MDE002":
			        case "MDE011":
			        case "MFL022":
			            TipoMic = "MI2";
			            CodigoMac = "MACCAB2220";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA515":
			        case "MCA514":
			            TipoMic = "MIC";
			            CodigoMac = "MACCAB2220";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA062":
			        case "MCA070":
			        case "MCA073":
			            TipoMic = "MID";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA068":
			            TipoMic = "MI3";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA069":
			            TipoMic = "MI1";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Maldonado";
			            break;
			        case "MCA513":
			            TipoMic = "MIC";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE001":
			        case "MDE004":
			        case "MDE008":
			        case "MDE009":
			        case "MDE006":
			            TipoMic = "MID";
			            CodigoMac = "MACDEV0340";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE005":
			        case "MDE502":
			            TipoMic = "MIC";
			            CodigoMac = "MACDEV0340";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE007":
			            TipoMic = "MI3";
			            CodigoMac = "MACDEV0340";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE011":
			            TipoMic = "MI2";
			            CodigoMac = "MACDEV0340";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE010":
			        case "MDE017":
			        case "MDE101":
			        case "MDE113":
			            TipoMic = "MID";
			            CodigoMac = "MACDEV2380";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE016A":
			            TipoMic = "MI2";
			            CodigoMac = "MACDEV2380";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE503":
			        case "MDE501":
			        case "MDE510":
			        case "MFL504":
			            TipoMic = "MIC";
			            CodigoMac = "MACDEV2380";
			            Cuenca = "Maldonado";
			            break;
			        case "MFL019":
			            TipoMic = "MI1";
			            CodigoMac = "MACFLO2230";
			            Cuenca = "Maldonado";
			            break;
			        case "MFL020":
			        case "MDE003":
			            TipoMic = "MI3";
			            CodigoMac = "MACFLO2230";
			            Cuenca = "Maldonado";
			            break;
			        case "MFL021":
			            TipoMic = "MI2";
			            CodigoMac = "MACFLO2230";
			            Cuenca = "Maldonado";
			            break;
			        case "MFL507":
			            TipoMic = "MIC";
			            CodigoMac = "MACFLO2230";
			            Cuenca = "Maldonado";
			            break;
			        case "MCE525":
			        case "MCA518":
			            TipoMic = "MIC";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Maldonado";
			            break;
			        case "MSA043":
			        case "MSA044":
			        case "MSA045":
			        case "MSA046":
			        case "MSA047":
			        case "MSA048":
			        case "MSA050":
			        case "MCE093":
			        case "MCA066":
			        case "MCA170":
			        case "MCA171":
			            TipoMic = "MID";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Maldonado";
			            break;
			        case "MSA065":
			        case "MCA071":
			        case "MCA072":
			            TipoMic = "MI1";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE014":
			        case "MSA054":
			        case "MSA059":
			        case "MSA060":
			            TipoMic = "MI3";
			            CodigoMac = "MACSAA0220";
			            Cuenca = "Maldonado";
			            break;
			        case "MDE015":
			        case "MSA055":
			        case "MSA056":
			        case "MSA150":
			            TipoMic = "MID";
			            CodigoMac = "MACSAA0220";
			            Cuenca = "Maldonado";
			            break;
			        case "MSA052":
			            TipoMic = "MI1";
			            CodigoMac = "MACSAA0220";
			            Cuenca = "Maldonado";
			            break;
			        case "MSA057":
			        case "MSA063":
			        case "MSA508":
			        case "MSA509":
			        case "MSA512":
			        case "MSA510":
			        case "MSA511":
			            TipoMic = "MIC";
			            CodigoMac = "MACSAA0220";
			            Cuenca = "Maldonado";
			            break;
			        case "MSA059":
			        case "MSA061":
			        case "MSA064":
			            TipoMic = "MI2";
			            CodigoMac = "MACSAA0220";
			            Cuenca = "Maldonado";
			            break;
			        case "MTR014":
			            TipoMic = "MI3";
			            CodigoMac = "MACMAT2390";
			            Cuenca = "Maldonado";
			            break;
			        case "MTR019":
			            TipoMic = "MI1";
			            CodigoMac = "MACFLO2310";
			            Cuenca = "Maldonado";
			            break;
			        case "MTR508":
			            TipoMic = "MIC";
			            CodigoMac = "MACFLO2310";
			            Cuenca = "Maldonado";
			            break;

			        case "MMO001":
			            TipoMic = "MI1";
			            CodigoMac = "MACMAT2380";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MMO002":
			        case "MMO004":
			        case "MMN010":
			            TipoMic = "MI2";
			            CodigoMac = "MACMAT2380";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MMO003":
			        case "MMO005":
			        case "MMO006":
			        case "MMO009":
			        case "MMN007":
			        case "MMN008":
			        case "MMN008":
			            TipoMic = "MI2";
			            CodigoMac = "MACMAT2380";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MMO510":
			        case "MMO511":
			        case "MMO512":
			            TipoMic = "MIC";
			            CodigoMac = "MACMOR0001";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSF001":
			        case "MSF002":
			            TipoMic = "MID";
			            CodigoMac = "MACSFE6010";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSF503":
			            TipoMic = "MIC";
			            CodigoMac = "MACSFE6010";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSF003":
			        case "MSF004":
			        case "MSF005":
			        case "MSF006":
			            TipoMic = "MID";
			            CodigoMac = "MACSFE3110";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSF504":
			        case "MSF502":
			            TipoMic = "MIC";
			            CodigoMac = "MACSFE3110";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSI001":
			        case "MSI002":
			        case "MSI003":
			        case "MSI004":
			        case "MSI005":
			        case "MSI006":
			        case "MSI007":
			            TipoMic = "MID";
			            CodigoMac = "MACVIC0010";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSI502":
			        case "MSI008":
			        case "MSI009":
			        case "MSF501":
			        case "MSI504":
			        case "MSI505":
			            TipoMic = "MIC";
			            CodigoMac = "MACVIC0010";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSM001":
			        case "MSM002":
			        case "MSM003":
			        case "MSM004":
			        case "MSM005":
			        case "MSM006":
			        case "MSM007":
			        case "MSM013":
			            TipoMic = "MID";
			            CodigoMac = "MACDEV2310";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSM008":
			        case "MSM010":
			        case "MSM011":
			        case "MSM012":
			            TipoMic = "MI3";
			            CodigoMac = "MACDEV2310";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSM009":
			            TipoMic = "MI12";
			            CodigoMac = "MACDEV2310";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSM505":
			        case "MSM506":
			        case "MSM507":
			        case "MSM509":
			            TipoMic = "MIC";
			            CodigoMac = "MACDEV2310";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MTI001":
			        case "MTI002":
			        case "MTI003":
			        case "MTI004":
			        case "MTI005":
			        case "MTI006":
			        case "MTI007":
			            TipoMic = "MID";
			            CodigoMac = "MACSFE0010";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MTR013":
			        case "MTR012":
			            TipoMic = "MID";
			            CodigoMac = "MACMAT2390";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MTR018":
			            TipoMic = "MI3";
			            CodigoMac = "MACFLO2310";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MTR507":
			        case "MMN011":
			        case "MMO506":
			            TipoMic = "MIC";
			            CodigoMac = "MACFLO2310";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL003":
			        case "MVL004":
			        case "MVL007":
			        case "MVL008":
			        case "MVL009":
			            TipoMic = "MID";
			            CodigoMac = "MACSAA0009";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL001":
			            TipoMic = "MI2";
			            CodigoMac = "MACSAA0009";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL005":
			            TipoMic = "MI1";
			            CodigoMac = "MACSAA0009";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL501":
			        case "MVL502":
			        case "MVL503":
			        case "MSI501":
			            TipoMic = "MIC";
			            CodigoMac = "MACSAA0009";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL006":
			        case "MSM015":
			        case "MSI501":
			            TipoMic = "MID";
			            CodigoMac = "MACSAA0390";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MSM014":
			        case "MVL010":
			        case "MVL013":
			            TipoMic = "MI3";
			            CodigoMac = "MACSAA0390";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL002":
			        case "MVL011":
			            TipoMic = "MI2";
			            CodigoMac = "MACSAA0390";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL012":
			        case "MVL014":
			            TipoMic = "MI1";
			            CodigoMac = "MACSAA0390";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MVL504":
			        case "MVL505":
			        case "MVL506":
			        case "MVL507":
			        case "MVL508":
			        case "MVL509":
			        case "MVL510":
			        case "MVL511":
			        case "MVL512":
			        case "MSM501":
			        case "MSM502":
			        case "MSM503":
			        case "MSM510":
			        case "MSM511":
			            TipoMic = "MIC";
			            CodigoMac = "MACSAA0390";
			            Cuenca = "Moron Reconquista";
			            break;
			        case "MAB001":
			        case "MAB002":
			            TipoMic = "MI1";
			            CodigoMac = "MACLAN2390";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAB003":
			        case "MLO010":
			        case "MLO011":
			        case "MLO002":
			        case "MLO007":
			        case "MEE001":
			            TipoMic = "MI3";
			            CodigoMac = "MACLAN2390";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAB501":
			        case "MLO501":
			        case "MLO503":
			        case "MLO504":
			        case "MLO507":
			        case "MLO508":
			            TipoMic = "MIC";
			            CodigoMac = "MACLAN2390";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAV502":
			        case "MAV503":
			            TipoMic = "MIC";
			            CodigoMac = "MACAVE0190";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAV001":
			        case "MAV002":
			        case "MAV005":
			            TipoMic = "MI2";
			            CodigoMac = "MACAVE0290";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAV004":
			            TipoMic = "MID";
			            CodigoMac = "MACAVE0290";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAV006":
			            TipoMic = "MI3";
			            CodigoMac = "MACAVE0290";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MAV501":
			        case "MAV513":
			        case "MAV514":
			        case "MAV515":
			            TipoMic = "MIC";
			            CodigoMac = "MACAVE0290";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCA074":
			        case "MCA075":
			            TipoMic = "MID";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCA516":
			        case "MCA517":
			            TipoMic = "MIC";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCA076":
			        case "MCA080":
			            TipoMic = "MI1";
			            CodigoMac = "MACCAB0280";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCA077":
			        case "MCA078":
			        case "MCA079":
			        case "MCO081":
			        case "MCO083":
			        case "MCO084":
			            TipoMic = "MI1";
			            CodigoMac = "MACCON0260";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCO520":
			            TipoMic = "MIC";
			            CodigoMac = "MACCON0260";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCE093":
			        case "MCO089":
			        case "MCO090":
			        case "MCO092":
			        case "MCO180":
			        case "MCO181":
			        case "MCO182":
			        case "MCO183":
			            TipoMic = "MID";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCO522":
			        case "MCO523":
			        case "MCA518":
			        case "MCA519":
			        case "MCO523":
			            TipoMic = "MIC";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCO086":
			            TipoMic = "MI3";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCO088":
			            TipoMic = "MI2";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCO091":
			            TipoMic = "MI1";
			            CodigoMac = "MACCON6310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MEE002":
			        case "MEE003":
			            TipoMic = "MID";
			            CodigoMac = "MACEEJ3110";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MEE501":
			            TipoMic = "MIC";
			            CodigoMac = "MACEEJ3110";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL025":
			            TipoMic = "MI2";
			            CodigoMac = "MACFLO2240";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL026":
			        case "MFL505":
			        case "MFL506":
			            TipoMic = "MIC";
			            CodigoMac = "MACFLO2240";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL030":
			        case "MFL035":
			        case "MFL036":
			        case "MFL120":
			        case "MFL121":
			            TipoMic = "MID";
			            CodigoMac = "MACFLO2240";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL031":
			        case "MFL034":
			            TipoMic = "MI3";
			            CodigoMac = "MACFLO2240";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL032":
			            TipoMic = "MI1";
			            CodigoMac = "MACFLO2240";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL036":
			        case "MFL038":
			        case "MFL039":
			            TipoMic = "MID";
			            CodigoMac = "MACCON0310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL037":
			            TipoMic = "MIC";
			            CodigoMac = "MACCON0310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MFL122":
			        case "MFL033":
			            TipoMic = "MID";
			            CodigoMac = "MACFLO2315";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MLA001":
			        case "MLA007":
			        case "MCO082":
			            TipoMic = "MI3";
			            CodigoMac = "MACAVE0395";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MLA003":
			        case "MLA009":
			            TipoMic = "MI1";
			            CodigoMac = "MACAVE0395";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MLA008":
			            TipoMic = "MI2";
			            CodigoMac = "MACAVE0395";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MCO521":
			            TipoMic = "MIC";
			            CodigoMac = "MACAVE0395";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMN020":
			        case "MMN021":
			        case "MMN022":
			        case "MMN023":
			        case "MMS026":
			            TipoMic = "MI3";
			            CodigoMac = "MACMAS2575";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMN024":
			            TipoMic = "MI1";
			            CodigoMac = "MACMAS2575";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS025":
			            TipoMic = "MI2";
			            CodigoMac = "MACMAS2575";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS027":
			            TipoMic = "MID";
			            CodigoMac = "MACMAS2575";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS505":
			            TipoMic = "MIC";
			            CodigoMac = "MACMAS2575";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS028":
			            TipoMic = "MID";
			            CodigoMac = "MACMAS2530";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS503":
			            TipoMic = "MIC";
			            CodigoMac = "MACMAS2530";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS031":
			        case "MMN030":
			            TipoMic = "MI3";
			            CodigoMac = "MACMAS2560";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS032":
			        case "MMS036":
			            TipoMic = "MI1";
			            CodigoMac = "MACMAS2560";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS033":
			        case "MMS34b":
			            TipoMic = "MID";
			            CodigoMac = "MACMAS2560";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMS035":
			        case "MMS037":
			        case "MMS038":
			        case "MMN029":
			            TipoMic = "MI2";
			            CodigoMac = "MACMAS2560";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MMN017":
			            TipoMic = "MID";
			            CodigoMac = "MACFLO2310";
			            Cuenca = "Matanza Riachuelo";
			            break;
			        case "MEE001":
			        case "MLO001":
			        case "MLO003":
			        case "MLO004":
			        case "MLO006":
			        case "MLO008":
			        case "MLO009":
			        case "MLO010":
			        case "MLA011":
			        case "MLO012":
			            TipoMic = "MID";
			            CodigoMac = "MACLAN2390";
			            Cuenca = "Salado";
			            break;
			        case "MLO005":
			            TipoMic = "MI1";
			            CodigoMac = "MACLAN2390";
			            Cuenca = "Salado";
			            break;
			        case "MLO502":
			        case "MLO505":
			        case "MLO506":
			        case "MLO507":
			        case "MLO508":
			        case "MLO509":
			            TipoMic = "MIC";
			            CodigoMac = "MACLAN2390";
			            Cuenca = "Salado";
			            break;
			        case "MAV003":
			        case "MAV007":
			            TipoMic = "MID";
			            CodigoMac = "MACAVE0190";
			            Cuenca = "Salado";
			            break;
			        case "MAV507":
			        case "MAV511":
			        case "MAV512":
			            TipoMic = "MIC";
			            CodigoMac = "MACAVE0190";
			            Cuenca = "Salado";
			            break;
			        case "MAV504":
			        case "MAV505":
			        case "MAV506":
			        case "MAV508":
			        case "MAV509":
			        case "MAV510":
			        case "MAV514":
			            TipoMic = "MIC";
			            CodigoMac = "MACAVE0290";
			            Cuenca = "Salado";
			            break;
			        case "MLA002":
			            TipoMic = "MI1";
			            CodigoMac = "MACAVE0395";
			            Cuenca = "Salado";
			            break;
			        case "MLA004":
			        case "MLA006":
			        case "MLA010":
			            TipoMic = "MID";
			            CodigoMac = "MACAVE0395";
			            Cuenca = "Salado";
			            break;
			        case "MQU002":
			        case "MQU003":
			        case "MQU004":
			        case "MQU005":
			        case "MQU007":
			        case "MQU008":
			        case "MQU009":
			        case "MQU010":
			        case "MQU033":
			            TipoMic = "MID";
			            CodigoMac = "MACQUI6410";
			            Cuenca = "Salado";
			            break;
			        case "MQU006":
			            TipoMic = "MI2";
			            CodigoMac = "MACQUI6410";
			            Cuenca = "Salado";
			            break;
			        case "MQUI035":
			        case "MQUI501":
			        case "MQUI502":
			        case "MQUI503":
			            TipoMic = "MIC";
			            CodigoMac = "MACQUI6410";
			            Cuenca = "Salado";
			            break;
			        case "MQU011":
			        case "MQU012":
			            TipoMic = "MIC";
			            CodigoMac = "MACQUI0420";
			            Cuenca = "Salado";
			            break;
			        case "MQU013":
			        case "MQU014":
			        case "MQU015":
			            TipoMic = "MID";
			            CodigoMac = "MACQUI2430";
			            Cuenca = "Salado";
			            break;
			        case "MQUI504":
			            TipoMic = "MIC";
			            CodigoMac = "MACQUI2430";
			            Cuenca = "Salado";
			            break;
			        case "MQU016":
			            TipoMic = "MIC";
			            CodigoMac = "MACQUI0440";
			            Cuenca = "Salado";
			            break;
			        case "MQU017":
			            TipoMic = "MI1";
			            CodigoMac = "MACQUI0440";
			            Cuenca = "Salado";
			            break;
			        case "MQU018":
			            TipoMic = "MID";
			            CodigoMac = "MACQUI0440";
			            Cuenca = "Salado";
			            break;
			        case "MQU019":
			        case "MQU020":
			            TipoMic = "MID";
			            CodigoMac = "MACQUI0410";
			            Cuenca = "Salado";
			            break;
			        case "MQU024":
			        case "MQU026":
			        case "MQU027":
			        case "MQU028":
			        case "MQU034":
			            TipoMic = "MID";
			            CodigoMac = "MACQUI2450";
			            Cuenca = "Salado";
			            break;
			        case "MQU025":
			            TipoMic = "MI1";
			            CodigoMac = "MACQUI2450";
			            Cuenca = "Salado";
			            break;
			        case "MQU029":
			        case "MQU030":
			        case "MQUI505":
			        case "MQUI507":
			        case "MQUI508":
			            TipoMic = "MIC";
			            CodigoMac = "MACQUI2450";
			            Cuenca = "Salado";
			            break;
			        default: TipoMic = "";
			            CodigoMac = "";
			            Cuenca = "";
			            break;
			    }
			    $("#" + TipoMicId).val(TipoMic);
			    $("#" + CodigoMacId).val(CodigoMac);
			    $("#" + CuencaId).val(Cuenca);
			}
			
			$(document).ready(function() {
				$("#ctl00_ContentPlaceHolder_InsertControl_btn_insertar").click(function() {
					ShowLoadingAnimation();
				});
			});
		
