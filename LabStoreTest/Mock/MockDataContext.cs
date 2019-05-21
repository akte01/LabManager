using LabStore.Dtos;
using LabStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStoreTest
{
    public class MockDataContext
    {
        public List<ReagentDto> ReagentDto
        {
            get
            {
                return new List<ReagentDto>
        {
            new ReagentDto
            {
                Id = 1,
                Name = "Amonu chlorek",
                InitialAmount = 500,
                ConsumedAmount = 200,
                FinalAmount = 300,
                StorageLocationId = 1,
                UnitId = 1
            },
            new ReagentDto
            {
                Id = 2,
                Name = "Amonu azotan",
                InitialAmount = 600,
                ConsumedAmount = 100,
                FinalAmount = 500,
                StorageLocationId = 1,
                UnitId = 1
            },
            new ReagentDto
            {
                Id = 3,
                Name = "Amonu węglan",
                InitialAmount = 1000,
                ConsumedAmount = 250,
                FinalAmount = 750,
                StorageLocationId = 1,
                UnitId = 1
            }
        };
            }
        }

        public List<Reagent> Reagent
        {
            get
            {
                return new List<Reagent>
        {
            new Reagent
            {
                Id = 1,
                Name = "Amonu chlorek",
                InitialAmount = 500,
                ConsumedAmount = 200,
                FinalAmount = 300,
                StorageLocationId = 1,
                UnitId = 1
            },
            new Reagent
            {
                Id = 2,
                Name = "Amonu azotan",
                InitialAmount = 600,
                ConsumedAmount = 100,
                FinalAmount = 500,
                StorageLocationId = 1,
                UnitId = 1
            },
            new Reagent
            {
                Id = 3,
                Name = "Amonu węglan",
                InitialAmount = 1000,
                ConsumedAmount = 250,
                FinalAmount = 750,
                StorageLocationId = 1,
                UnitId = 1
            }
        };
            }
        }

        public List<OrderDto> OrderDto
        {
            get
            {
                return new List<OrderDto>
        {
            new OrderDto
            {
                Id = 1,
                Date = new DateTime(2019, 03, 27, 10, 22, 49),
                Content = "Roztwory: Odczynniki stałe: Sprzęt: aerometry 3 szt.; płyty żeliwne - podgrzewacze 2 szt.;",
                StatusId = 1,
                DateFor = new DateTime(2019, 03, 29, 11, 00, 00),
                Grade = "3a",
                UserId = "abc123",
                Comment = ""
            },
            new OrderDto
            {
                Id = 2,
                Date = new DateTime(2019, 04, 24, 09, 15, 14),
                Content = "Roztwory: Azotan srebra 0,10 mol/L 400 mL; Odczynniki stałe: Sprzęt:",
                StatusId = 1,
                DateFor = new DateTime(2019, 04, 26, 12, 00, 00),
                Grade = "4at",
                UserId = "def456",
                Comment = "Pilne!"
            },
        };
            }
        }

        public List<Order> Order
        {
            get
            {
                return new List<Order>
        {
            new Order
            {
                Id = 1,
                Date = new DateTime(2019, 03, 27, 10, 22, 49),
                Content = "Roztwory: Odczynniki stałe: Sprzęt: aerometry 3 szt.; płyty żeliwne - podgrzewacze 2 szt.;",
                StatusId = 1,
                DateFor = new DateTime(2019, 03, 29, 11, 00, 00),
                Grade = "3a",
                UserId = "abc123",
                Comment = ""
            },
            new Order
            {
                Id = 2,
                Date = new DateTime(2019, 04, 24, 09, 15, 14),
                Content = "Roztwory: Azotan srebra 0,10 mol/L 400 mL; Odczynniki stałe: Sprzęt:",
                StatusId = 1,
                DateFor = new DateTime(2019, 04, 26, 12, 00, 00),
                Grade = "4at",
                UserId = "def456",
                Comment = "Pilne!"
            },
        };
            }
        }

        public List<Unit> Unit
        {
            get
            {
                return new List<Unit>
        {
            new Unit
            {
                Id = 1,
                Name = "g"

            },
            new Unit
            {
                Id = 2,
                Name = "mL"
            },
            new Unit
            {
                Id = 3,
                Name = "szt."
            }
        };
            }
        }

        public List<ConcentrationUnit> ConcentrationUnit
        {
            get
            {
                return new List<ConcentrationUnit>
        {
            new ConcentrationUnit
            {
                Id = 1,
                Name = "mol/L"

            },
            new ConcentrationUnit
            {
                Id = 2,
                Name = "%"
            }
        };
            }
        }

        public List<StorageLocation> StorageLocation
        {
            get
            {
                return new List<StorageLocation>
        {
            new StorageLocation
            {
                Id = 1,
                Name = "Nieorganiczne, P. Lab."

            },
            new StorageLocation
            {
                Id = 2,
                Name = "Organiczne, P. Lab."
            },
            new StorageLocation
            {
                Id = 3,
                Name = "Ciekle, P. Lab."
            },
            new StorageLocation
            {
                Id = 4,
                Name = "Magazyn, P. Lab."
            },
            new StorageLocation
            {
                Id = 5,
                Name = "Magazyn, Pracownia 4"
            },
            new StorageLocation
            {
                Id = 6,
                Name = "Magazyn, Pracownia 2"
            }
        };
            }
        }

        public List<Equipment> Equipment
        {
            get
            {
                return new List<Equipment>
        {
            new Equipment
            {
                Id = 1,
                Name = "Mieszadła magnetyczne",
                Amount = 10,
                EquipmentLocationId = 1,
                Comment = ""
            },
            new Equipment
            {
                Id = 2,
                Name = "Gruszka na pipetę",
                Amount = 5,
                EquipmentLocationId = 5,
                Comment = "Szafa egzaminacyjna"
            },
            new Equipment
            {
                Id = 3,
                Name = "Spektrofotometr",
                Amount = 2,
                EquipmentLocationId = 2,
                Comment = ""
            }
        };
            }
        }

        public List<EquipmentDto> EquipmentDto
        {
            get
            {
                return new List<EquipmentDto>
        {
            new EquipmentDto
            {
                Id = 1,
                Name = "Mieszadła magnetyczne",
                Amount = 10,
                EquipmentLocationId = 1,
                Comment = ""
            },
            new EquipmentDto
            {
                Id = 2,
                Name = "Gruszka na pipetę",
                Amount = 5,
                EquipmentLocationId = 5,
                Comment = "Szafa egzaminacyjna"
            },
            new EquipmentDto
            {
                Id = 3,
                Name = "Spektrofotometr",
                Amount = 2,
                EquipmentLocationId = 2,
                Comment = ""
            }
        };
            }
        }

        public List<EquipmentLocation> EquipmentLocation
        {
            get
            {
                return new List<EquipmentLocation>
        {
            new EquipmentLocation
            {
                Id = 1,
                Name = "Pracownia 1"

            },
            new EquipmentLocation
            {
                Id = 2,
                Name = "Pracownia 2"
            },
            new EquipmentLocation
            {
                Id = 3,
                Name = "Pracownia 4"
            },
            new EquipmentLocation
            {
                Id = 4,
                Name = "Pracownia 6"
            },
            new EquipmentLocation
            {
                Id = 5,
                Name = "Szafa korytarz"
            },
            new EquipmentLocation
            {
                Id = 6,
                Name = "Szafa metalowe"
            },
            new EquipmentLocation
            {
                Id = 7,
                Name = "Pokój laborantów"
            },
            new EquipmentLocation
            {
                Id = 8,
                Name = "Pokój wagowy"
            }
        };
            }
        }

        public List<ApplicationUser> ApplicationUser
        {
            get
            {
                return new List<ApplicationUser>
        {
            new ApplicationUser
            { 
                Id = "abc123",
                Name = "Jan",
                Surname = "Kowalski",
                Email = "kowalski@labmanager.pl",
                PasswordHash = "janKowalski1"
            },
            new ApplicationUser
            {
                Id = "def456",
                Name = "Krystyna",
                Surname = "Nowak",
                Email = "nowak@labmanager.pl",
                PasswordHash = "krystynaNowak2"
            },
            new ApplicationUser
            {
                Id = "ghi789",
                Name = "Zofia",
                Surname = "Sienkiewicz",
                Email = "sienkiewicz@labmanager.pl",
                PasswordHash = "zofiaSienkiewicz3"
            }
        };
            }
        }

        public List<OrderBase> OrderBase
        {
            get
            {
                return new List<OrderBase>
        {
            new OrderBase
            {
                Id = 0,
                UserId = "0",
                DateFor = new DateTime(2000, 01, 01, 00, 00, 00),
                Grade = "",
                Comment = ""
            },
            new OrderBase
            {
                Id = 1,
                UserId = "abc123",
                DateFor = new DateTime(2019, 03, 29, 11, 00, 00),
                Grade = "3a",
                Comment = ""
            },
            new OrderBase
            {
                Id = 2,
                UserId = "def456",
                DateFor = new DateTime(2019, 04, 26, 12, 00, 00),
                Grade = "4at",
                Comment = "Pilne!"
            }
        };
            }
        }

        public List<SolidOrder> SolidOrder
        {
            get
            {
                return new List<SolidOrder>
        {
            new SolidOrder
            {
                Id = 0,
                UserId = "0",
                Name = "",
                Amount = 0,
                UnitId = 0
            },
            new SolidOrder
            {
                Id = 1,
                UserId = "abc123",
                Name = "Amonu chlorek",
                Amount = 10,
                UnitId = 1
            },
            new SolidOrder
            {
                Id = 2,
                UserId = "def456",
                Name = "Amonu azotan",
                Amount = 20,
                UnitId = 1
            }
        };
            }
        }

        public List<SolutionOrder> SolutionOrder
        {
            get
            {
                return new List<SolutionOrder>
        {
            new SolutionOrder
            {
                Id = 0,
                UserId = "0",
                Name = "",
                Concentration = 0,
                ConcentrationUnitId = 0
            },
            new SolutionOrder
            {
                Id = 1,
                UserId = "abc123",
                Name = "Amonu chlorek",
                Concentration = 1,
                Volume = 100,
                ConcentrationUnitId = 1
            },
            new SolutionOrder
            {
                Id = 2,
                UserId = "def456",
                Name= "Azotan srebra",
                Concentration = 2,
                Volume = 1000,
                ConcentrationUnitId = 2
            }
        };
            }
        }

        public List<EquipmentOrder> EquipmentOrder
        {
            get
            {
                return new List<EquipmentOrder>
        {
            new EquipmentOrder
            {
                Id = 0,
                UserId = "0",
                Name = "",
                Amount = 0,
            },
            new EquipmentOrder
            {
                Id = 1,
                UserId = "abc123",
                Name = "Płyty grzewcze",
                Amount = 5,
            },
            new EquipmentOrder
            {
                Id = 2,
                UserId = "def456",
                Name = "Biurety 50 ml",
                Amount = 10,
            }
        };
            }
        }
    }
}
